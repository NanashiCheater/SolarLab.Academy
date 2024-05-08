using SolarLab.Academy.Domain.Announcements.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Domain.Announcements.Files
{
    /// <summary>
    /// Изображение.
    /// </summary>
    public class AnnouncementImage
    {
        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public Guid AnnouncementId { get; set; }

        /// <summary>
        /// Идентификатор изображения.
        /// </summary>
        public Guid ImageId { get; set; }

        /// <summary>
        /// Объявление.
        /// </summary>
        public Announcement Announcement { get; set; }

        /// <summary>
        /// Изображение.
        /// </summary>
        public Domain.Files.Entity.File Image { get; set; }
    }
}
