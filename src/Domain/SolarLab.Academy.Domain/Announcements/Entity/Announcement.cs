using SolarLab.Academy.Domain.Base;
using SolarLab.Academy.Domain.Categories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Domain.Announcements.Entity
{
    /// <summary>
    /// Объявление.
    /// </summary>
    public class Announcement: BaseEntity
    {
        /// <summary>
        /// Идентификатор пользователя создавшего объявление.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор категории товара.
        /// </summary>
        public Guid CategoryId {  get; set; }

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
        public int Cost { get; set; }

        /// <summary>
        /// Создатель объявления.
        /// </summary>
        public Users.Entity.User? Owner { get; set; } 

        /// <summary>
        /// Категория товара.
        /// </summary>
        public Category? Category { get; set; }
    }
}
