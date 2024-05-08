using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Contracts.Users
{
    /// <summary>
    /// Запрос на авторизацию.
    /// </summary>
    public class AuthenticateRequest
    {
        /// <summary>
        /// Почта.
        /// </summary>
        public string Email {  get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
    }
}
