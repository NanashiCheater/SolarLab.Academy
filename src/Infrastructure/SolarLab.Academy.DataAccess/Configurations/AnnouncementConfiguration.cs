using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarLab.Academy.Domain.Announcements.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Announcements.Entity.Announcement"/>.
    /// </summary>
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Domain.Announcements.Entity.Announcement>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Domain.Announcements.Entity.Announcement> builder)
        {
            builder.ToTable("Announcements");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e=>e.Name).IsRequired().HasMaxLength(255);

            builder.Property(e => e.Description).IsRequired().HasMaxLength(2550);

            builder.Property(e => e.Cost).IsRequired();

            builder.Property(e => e.UserId).IsRequired();

            builder.Property(e => e.CategoryId).IsRequired();

            builder.HasOne(e => e.Owner).WithMany().HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Category).WithMany().HasForeignKey(e => e.CategoryId);
        }
    }
}
