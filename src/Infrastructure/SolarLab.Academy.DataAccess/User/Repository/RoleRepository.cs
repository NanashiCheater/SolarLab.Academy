using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.DataAccess.Base;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess.User.Repository
{
    public class RoleRepository : BaseRepository<ApplicationRole>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}
