using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artour.Domain.Models.Configurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.ToTable("tour");

            builder.Property(u => u.TourId).HasColumnName("tour_id");
            builder.HasKey(u => u.TourId);

            builder.Property(u => u.OwnerId).HasColumnName("owner_id");
            builder.Property(u => u.Title).HasColumnName("title");
            builder.Property(u => u.Description).HasColumnName("description");
            builder.Property(u => u.CityId).HasColumnName("city_id");
        }
    }
}
