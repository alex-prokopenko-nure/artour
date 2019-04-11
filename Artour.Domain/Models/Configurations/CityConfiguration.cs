using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("city");

            builder.Property(x => x.CityId).HasColumnName("city_id");
            builder.HasKey(x => x.CityId);

            builder.Property(x => x.CountryId).HasColumnName("country_id");
            builder.Property(x => x.Name).HasColumnName("name");
        }
    }
}
