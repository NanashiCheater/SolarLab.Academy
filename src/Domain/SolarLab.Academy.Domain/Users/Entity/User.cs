using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Domain.Users.Entity
{
    public class User : IdentityUser
    {
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
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Регион пользователя.
        /// </summary>
        public int Region {  get; set; }

        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
    /// <summary>
    /// Пользовательская роль.
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        public Guid Id { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
    /// <summary>
    /// Связь роли и пользователя.
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
    /// <summary>
    /// Полизовательские клеймы.
    /// </summary>
    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
        public virtual User User { get; set; }
    }
    /// <summary>
    /// Логин провайдер.
    /// </summary>
    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public virtual User User { get; set; }
    }
    /// <summary>
    /// Роли и клеймы.
    /// </summary>
    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual ApplicationRole Role { get; set; }
    }
    /// <summary>
    /// Токены.
    /// </summary>
    public class ApplicationUserToken : IdentityUserToken<Guid>
    {
        public virtual User User { get; set; }
    }
}
