using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.DataAccess.Base;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess.User.Repository
{
    public class UserRoleRepository : BaseRepository<ApplicationUserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext context) : base(context)
        {
        }
        /// <inheritdoc />
        public async Task DeleteUserRolesById(Guid Id, CancellationToken cancellationToken)
        {
            var roles = GetAll().Where(s => s.UserId == Id);
            DbSet.RemoveRange(roles);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
