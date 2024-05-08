using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Contracts.Announcements
{
    /// <summary>
    /// Запрос на обновление фотографий объявления.
    /// </summary>
    public class UpdateImagesRequest
    {
        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Изображения объявления.
        /// </summary>
        public List<IFormFile?> Images { get; set; } = new List<IFormFile?>();
    }
}
