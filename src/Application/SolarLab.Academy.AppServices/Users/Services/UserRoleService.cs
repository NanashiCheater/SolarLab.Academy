using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Users.Services
{
    /// <inheritdoc />
    public class UserRoleService:IUserRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleService(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }
        /// <inheritdoc />
        public async Task DeleteUserRolesById(Guid Id, CancellationToken cancellationToken)
        {
            await _roleRepository.DeleteAsync(Id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<ApplicationRole> GetUserRoleById(Guid id, CancellationToken cancellationToken)
        {
            var userRole = await _userRoleRepository.GetAll().Where(s => s.UserId == id).FirstOrDefaultAsync(cancellationToken);
            if (userRole == null)
            {
                throw new NullReferenceException();
            }
            var role = await _roleRepository.GetAll().Where(s => s.Id == userRole.RoleId).FirstOrDefaultAsync(cancellationToken);
            if (role == null)
            {
                throw new NullReferenceException();
            }
            return role;
        }
    }
}
