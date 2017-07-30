using HashidsNet;
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
    /// Получить запись с оригинальной ссылкой по короткой
    /// </summary>
    public class FindUrlByShort: IQueryHandler<FindUrlByShort.Query, Url>
    {
        public class Query: IQuery<Url>
        {
            public long UserId { get; set; }
            public DateTimeOffset Created { get; set; }
        }

        public UrlsContext Db { get; set; }

        public Url Handle(Query query)
        {
            return Db.Urls
                .Where(u => u.UserId == query.UserId)
                .Where(u => u.Created == query.Created)
                .FirstOrDefault();
        }
    }
}
