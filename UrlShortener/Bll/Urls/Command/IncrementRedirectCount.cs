using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Bll.Infrastructure.Cqrs;
using UrlShortener.Bll.Model;

namespace UrlShortener.Bll.Urls.Command
{
    /// <summary>
    /// Увеличить число посещений ссылки
    /// </summary>
    public class IncrementRedirectCount: ICommandHandler<IncrementRedirectCount.Command>
    {
        public class Command: ICommand
        {
            /// <summary>
            /// Id записи со ссылкой
            /// </summary>
            public long Id { get; set; }
        }

        public UrlsContext Db { get; set; }

        public void Handle(Command command)
        {
            var sql = @"update [dbo].[Urls] set RedirectCount = RedirectCount + 1 where Id = {0}";
            Db.Database.ExecuteSqlCommand(sql, command.Id);
        }
    }
}
