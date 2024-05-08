using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess.Configurations
{
    public class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserLogin> b)
        {
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            b.Property(l => l.LoginProvider).HasMaxLength(128);

            b.Property(l => l.ProviderKey).HasMaxLength(128);

            b.ToTable("AspNetUserLogins");
        }
    }
}
