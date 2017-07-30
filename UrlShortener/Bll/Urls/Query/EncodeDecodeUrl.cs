using HashidsNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Bll.Infrastructure.Cqrs;

namespace UrlShortener.Bll.Urls.Query
{
    /// <summary>
    /// Обработчик команд кодирования и декодирования ссылки.
    /// </summary>
    /// <remarks>
    /// У меня нет чёткого представления, на основе чего генерить короткий URL,
    /// поэтому, пусть это будет пара значений: Id пользователя и время создания ссылки.
    /// Ограничение в этом случае: нельзя создавать ссылки одним пользователем в абсолютно одно и то же время.
    /// </remarks>
    public class EncodeDecodeUrl: IQueryHandler<EncodeDecodeUrl.Encode, string>,
        IQueryHandler<EncodeDecodeUrl.Decode, EncodeDecodeUrl.DecodeResult>
    {
        /// <summary>
        /// Соль для кодирования через Hashids
        /// </summary>
        /// <remarks>
        /// Должна храниться в конфиге. Для упрощения объявлена константой.
        /// </remarks>
        internal const string HASHIDS_SALT = "Hashids salt 123";

        /// <summary>
        /// Параметры кодирования ссылки
        /// </summary>
        public class Encode: IQuery<string>
        {
            public long UserId { get; set; }
            public DateTimeOffset Created { get; set; }
        }

        /// <summary>
        /// Параметры декодирования ссылки
        /// </summary>
        public class Decode: IQuery<DecodeResult>
        {
            public string ShortUrl { get; set; }
        }

        /// <summary>
        /// Результат декодирования ссылки
        /// </summary>
        public class DecodeResult
        {
            public long UserId { get; set; }
            public DateTimeOffset Created { get; set; }
        }

        public string Handle(Encode query)
        {
            var minTime = DateTimeOffset.MinValue;
            var ticks = (long)(query.Created - minTime).Ticks;
            var shortUrl = new Hashids(HASHIDS_SALT).EncodeLong(query.UserId, ticks);
            return shortUrl;
        }

        public DecodeResult Handle(Decode query)
        {
            var numbers = new Hashids(HASHIDS_SALT).DecodeLong(query.ShortUrl);
            var userId = numbers[0];
            var ticks = numbers[1];
            var created = DateTimeOffset.MinValue + TimeSpan.FromTicks(ticks);
            created = created.ToLocalTime();
            return new DecodeResult
            {
                UserId = userId,
                Created = created
            };
        }
    }
}
