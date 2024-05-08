using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarLab.Academy.Domain.Comments.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Text).IsRequired().HasMaxLength(2550);

            builder.Property(e => e.UserId).IsRequired();

            builder.Property(e => e.AnnouncementId).IsRequired();

            builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Announcement).WithMany().HasForeignKey(e => e.AnnouncementId);
        }
    }
}
