using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Contracts.Announcements
{
    /// <summary>
    /// Запрос на обновление информации объявления.
    /// </summary>
    public class UpdateInfoRequest
    {
        /// <summary>
        /// Идентификатор оюъявления.
        /// </summary>
        public Guid Id { get; set; }

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
    }
}
