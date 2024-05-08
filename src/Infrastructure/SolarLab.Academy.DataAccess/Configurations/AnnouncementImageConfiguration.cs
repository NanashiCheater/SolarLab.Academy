using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarLab.Academy.Domain.Announcements.Entity;
using SolarLab.Academy.Domain.Announcements.Files;
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
    public class AnnouncementImageConfiguration : IEntityTypeConfiguration<Domain.Announcements.Files.AnnouncementImage>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AnnouncementImage> builder)
        {
            builder.ToTable("AnnouncementImages");

            builder.HasKey(l => new { l.AnnouncementId, l.ImageId });

            builder.HasOne(e => e.Announcement).WithMany().HasForeignKey(e => e.AnnouncementId);

            builder.HasOne(e => e.Image).WithMany().HasForeignKey(e => e.ImageId);
        }
    }
}
