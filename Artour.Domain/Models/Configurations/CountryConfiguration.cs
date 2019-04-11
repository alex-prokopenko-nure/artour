using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("country");

            builder.Property(x => x.CountryId).HasColumnName("country_id");
            builder.HasKey(x => x.CountryId);

            builder.Property(x => x.Code).HasColumnName("code");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.RegionId).HasColumnName("region_id");
        }
    }
}
