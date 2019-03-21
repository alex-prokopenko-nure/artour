using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artour.Domain.Models.Configurations
{
    public class SightConfiguration : IEntityTypeConfiguration<Sight>
    {
        public void Configure(EntityTypeBuilder<Sight> builder)
        {
            builder.ToTable("sight");

            builder.Property(u => u.SightId).HasColumnName("sight_id");
            builder.HasKey(u => u.SightId);

            builder.Property(u => u.TourId).HasColumnName("tour_id");
        }
    }
}
