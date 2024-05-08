using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Contracts.Announcements
{
    /// <summary>
    /// Запрос на поиск объявлений по характеристикам.
    /// </summary>
    public class SearchAnnouncementRequest
    {
        /// <summary>
        /// Поисковое слово.
        /// </summary>
        public string SearchWord { get; set; }
       
        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Количечество сущностей на странице.
        /// </summary>
        public int? Batchsize { get; set; } = 10;

    }
}
