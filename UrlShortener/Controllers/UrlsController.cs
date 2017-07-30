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
	[Route("api/urls")]
	public class UrlsController : Controller
	{
		public IQueryProcessor QueryProcessor { get; set; }
		public ICommandProcessor CommandProcessor { get; set; }

		/// <summary>
		/// Пока без регистрации - заглушка
		/// </summary>
		const long CURRENT_USER_ID = 1;

		[HttpGet]
		public IEnumerable<Url> GetAll()
		{
			return QueryProcessor.Process(new FindUrlsByUser.Query
			{
				UserId = CURRENT_USER_ID
			});
		}

		[HttpPost]
		public void Post([FromBody]Url value)
		{
			var now = DateTime.Now;

			var shortUrl = QueryProcessor.Process(new EncodeDecodeUrl.Encode
			{
				Created = now,
				UserId = CURRENT_USER_ID
			});

			CommandProcessor.Process(new CreateShortUrl.Command
			{
				Created = now,
				UserId = CURRENT_USER_ID,
				OriginalUrl = value.Original,
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

            return Redirect(url.Original);
		}
	}
}
