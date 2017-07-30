using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Bll.Model
{
    /// <summary>
    /// Контекст базы данных Urls
    /// </summary>
    public class UrlsContext : DbContext
    {
        public UrlsContext(DbContextOptions<UrlsContext> options)
        : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Url> Urls { get; set; }
    }
}
