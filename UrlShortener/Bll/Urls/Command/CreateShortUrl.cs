using HashidsNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Bll.Infrastructure.Cqrs;
using UrlShortener.Bll.Model;

namespace UrlShortener.Bll.Urls.Command
{
    /// <summary>
    /// Создать запись с короткой ссылкой
    /// </summary>
    public class CreateShortUrl: ICommandHandler<CreateShortUrl.Command>
    {
        public class Command: ICommand
        {
            public string OriginalUrl { get; set; }
            public string ShortUrl { get; set; }
            public DateTimeOffset Created { get; set; }
            public long UserId { get; set; }
        }

        public UrlsContext Db { get; set; }

        public void Handle(Command command)
        {
            Db.Urls.Add(new Url
            {
                Created = command.Created,
                Original = command.OriginalUrl,
                Short = command.ShortUrl,
                UserId = command.UserId
            });

            Db.SaveChanges();
        }
    }
}
