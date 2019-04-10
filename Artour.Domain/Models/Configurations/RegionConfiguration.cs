using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("region");

            builder.Property(x => x.RegionId).HasColumnName("region_id");
            builder.HasKey(x => x.RegionId);

            builder.Property(x => x.Name).HasColumnName("name");
        }
    }
}
