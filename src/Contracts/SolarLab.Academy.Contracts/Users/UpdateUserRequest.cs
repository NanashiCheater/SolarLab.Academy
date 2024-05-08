using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Contracts.Users
{
    /// <summary>
    /// Запрос на обновление данных пользователя.
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Регион проживания.
        /// </summary>
        public int? Region { get; set; }

        /// <summary>
        /// Телефонный номер пользователя.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Адрес электронной почты.
        /// </summary>
        public string Email { get; set; }
    }
}
