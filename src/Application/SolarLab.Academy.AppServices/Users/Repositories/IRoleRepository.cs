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
    /// Репозиторий для работы с ролями.
    /// </summary>
    public interface IRoleRepository: IBaseRepository<ApplicationRole>
    {
    }
}
