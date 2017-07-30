using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Bll.Model
{
    /// <summary>
    /// Информация о ссылке
    /// </summary>
    public class Url
    {
        public long Id { get; set; }

        [StringLength(2000)]
        public string Original { get; set; }

        [StringLength(26)]
        public string Short { get; set; }

        public long UserId { get; set; }

        public DateTimeOffset Created { get; set; }

        public long RedirectCount { get; set; }
    }
}
