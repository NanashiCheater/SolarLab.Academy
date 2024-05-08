using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SolarLab.Academy.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Users.Entity.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Users.Entity.User> builder)
        {
            builder
                .ToTable("Users")
                .HasKey(t => t.Id);

            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            builder.ToTable("AspNetUsers");

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

        
            builder.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            builder.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            builder.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(x => x.MiddleName)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(x => x.BirthDate)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .IsRequired();

            builder
                .Property(x => x.PhoneNumber)
                .IsRequired();
        }
    }

}
