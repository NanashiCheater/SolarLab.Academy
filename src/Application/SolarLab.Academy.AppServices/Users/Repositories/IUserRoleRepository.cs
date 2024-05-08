using SolarLab.Academy.AppServices.Base;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Users.Repositories
{
    /// <summary>
    /// Репозиторий для работы со связью пользователя и его роли.
    /// </summary>
    public interface IUserRoleRepository : IBaseRepository<ApplicationUserRole>
    {
       /// <summary>
       /// Удалить роли пользователя.
       /// </summary>
       public Task DeleteUserRolesById(Guid Id, CancellationToken cancellationToken); 
    }
}
