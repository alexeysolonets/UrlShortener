using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Bll;
using UrlShortener.Bll.Infrastructure.Cqrs;
using UrlShortener.Bll.Model;
using UrlShortener.Bll.Urls.Command;
using UrlShortener.Bll.Urls.Query;

namespace UrlShortener.Controllers
{
	/// <summary>
	/// Контроллер ссылок
	/// </summary>
	/// <remarks>
	/// В настоящем проекте вместо моделей из слоя Bll.Model я бы использовал специальные DTO
	/// </remarks>
    [Produces("application/json")]
	public class UrlsController : Controller
	{
		public IQueryProcessor QueryProcessor { get; set; }
		public ICommandProcessor CommandProcessor { get; set; }

		/// <summary>
		/// Пока без регистрации - заглушка
		/// </summary>
		const long CURRENT_USER_ID = 1;

		[HttpGet]
		[Route("api/urls")]
		public IEnumerable<Url> GetAll()
		{
			return QueryProcessor.Process(new FindUrlsByUser.Query
			{
				UserId = CURRENT_USER_ID
			});
		}

		[HttpPost]
		[Route("api/urls")]
		public void Post([FromBody]Url value)
		{
			var now = DateTime.Now;

            var shortUrl = QueryProcessor.Process(new EncodeDecodeUrl.Encode
			{
				Created = now,
				UserId = CURRENT_USER_ID
			});

            var original = FixUrl(value.Original);

			CommandProcessor.Process(new CreateShortUrl.Command
			{
				Created = now,
				UserId = CURRENT_USER_ID,
                OriginalUrl = original,
				ShortUrl = shortUrl
			});
		}

		[Route("go/{shortUrl}")]
		[HttpGet]
		public ActionResult Go(string shortUrl)
		{
			var decoded = QueryProcessor.Process(new EncodeDecodeUrl.Decode
			{
				ShortUrl = shortUrl
			});

			if (decoded == null)
			{
                return NotFound();
			}

			var url = QueryProcessor.Process(new FindUrlByShort.Query
			{
				Created = decoded.Created,
				UserId = decoded.UserId
			});

            if (url == null)
            {
                return NotFound();
            }

			CommandProcessor.Process(new IncrementRedirectCount.Command
			{
				Id = url.Id
			});

            var original = this.FixUrl(url.Original);

            return Redirect(original);
		}

        /// <summary>
        /// Fixs the URL.
        /// </summary>
        /// <returns>The URL.</returns>
        /// <param name="url">URL.</param>
        private string FixUrl(string url) 
        {
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri uri))
            {
				if (!uri.IsAbsoluteUri) 
				{
					return new Uri("http://" + uri).AbsoluteUri;
				}
            }

            return url;
        }
	}
}
