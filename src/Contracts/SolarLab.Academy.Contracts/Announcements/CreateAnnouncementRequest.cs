using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Contracts.Announcements
{
    /// <summary>
    /// Запрос на создание объявления.
    /// </summary>
    public class CreateAnnouncementRequest
    {
        /// <summary>
        /// Идентификатор пользователя создавшего объявление.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор категории товара.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Название товара.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание товара.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Стоимость товара в копейках.
        /// </summary>
        public int? Cost { get; set; }
        
        /// <summary>
        /// Изображения объявления.
        /// </summary>
        public List<IFormFile?> Images { get; set; } = new List<IFormFile?>();
    }
}
