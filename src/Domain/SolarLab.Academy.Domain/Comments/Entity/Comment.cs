using SolarLab.Academy.Domain.Announcements.Entity;
using SolarLab.Academy.Domain.Base;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Domain.Comments.Entity
{
    /// <summary>
    /// Комментарий.
    /// </summary>
    public class Comment: BaseEntity
    {
        /// <summary>
        /// Идентификатор написавшего пользователя.
        /// </summary>
        public Guid UserId {  get; set; } 

        /// <summary>
        /// Идентификатор объявления, на которое написан коментарий.
        /// </summary>
        public Guid AnnouncementId { get; set; }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Пользователь, написавший комментарий.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Объявление, на которое написан коментарий.
        /// </summary>
        public Announcement Announcement { get; set; }
    }
}
