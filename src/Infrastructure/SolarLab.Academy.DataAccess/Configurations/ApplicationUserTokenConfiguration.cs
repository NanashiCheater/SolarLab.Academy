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
    public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserToken> b)
        {
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            b.Property(t => t.LoginProvider).HasMaxLength(512);

            b.Property(t => t.Name).HasMaxLength(512);

            b.ToTable("AspNetUserTokens");
        }
    }
}
