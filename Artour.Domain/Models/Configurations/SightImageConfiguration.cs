using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artour.Domain.Models.Configurations
{
    public class SightImageConfiguration : IEntityTypeConfiguration<SightImage>
    {
        public void Configure(EntityTypeBuilder<SightImage> builder)
        {
            builder.ToTable("sight_image");

            builder.Property(u => u.SightImageId).HasColumnName("sight_image_id");
            builder.HasKey(u => u.SightImageId);

            builder.Property(u => u.SightId).HasColumnName("sight_id");
            builder.Property(u => u.Description).HasColumnName("description");
            builder.Property(u => u.FileSize).HasColumnName("file_size");
            builder.Property(u => u.FullFilename).HasColumnName("full_file_name");
            builder.Property(u => u.Order).HasColumnName("order");
            builder.Property(u => u.UploadedOn).HasColumnName("uploaded_on");
        }
    }
}
