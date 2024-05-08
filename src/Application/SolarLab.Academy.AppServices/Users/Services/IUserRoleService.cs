using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Users.Services
{
    /// <summary>
    /// Сервис для работы со связью пользователя и роли.
    /// </summary>
    public interface IUserRoleService
    {
        /// <summary>
        /// Получить роль пользователя по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<ApplicationRole> GetUserRoleById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить роли пользователя.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteUserRolesById(Guid Id, CancellationToken cancellationToken);
    }
}
