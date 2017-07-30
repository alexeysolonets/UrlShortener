using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Bll.Infrastructure.Cqrs;
using UrlShortener.Bll.Model;

namespace UrlShortener.Bll.Urls.Query
{
    /// <summary>
    /// Найти все ссылки указанного пользователя
    /// </summary>
    public class FindUrlsByUser : IQueryHandler<FindUrlsByUser.Query, IEnumerable<Url>>
    {
        public class Query : IQuery<IEnumerable<Url>>
        {
            public long UserId { get; set; }
        }

        public UrlsContext Db { get; set; }

        public IEnumerable<Url> Handle(Query query)
        {
            return Db.Urls
                .Where(url => url.UserId == query.UserId)
                .ToList();
        }
    }
}
