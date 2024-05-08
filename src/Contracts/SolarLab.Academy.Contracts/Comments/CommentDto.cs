using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Contracts.Comments
{
    /// <summary>
    /// Комментарий.
    /// </summary>
    public class CommentDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Идентификатор написавшего пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор объявления, на которое написан коментарий.
        /// </summary>
        public Guid AnnouncementId { get; set; }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        public string Text { get; set; }
    }
}
