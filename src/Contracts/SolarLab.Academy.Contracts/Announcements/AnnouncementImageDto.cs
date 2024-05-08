using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Contracts.Announcements
{
    /// <summary>
    /// Изображение объявления.
    /// </summary>
    public class AnnouncementImageDto
    {
        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public Guid AnnouncementId { get; set; }

        /// <summary>
        /// Идентификатор изображения.
        /// </summary>
        public Guid ImageId { get; set; }
    }
}
