using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artour.Domain.Models.Configurations
{
    public class SightSeenConfiguration : IEntityTypeConfiguration<SightSeen>
    {
        public void Configure(EntityTypeBuilder<SightSeen> builder)
        {
            builder.ToTable("sight_seen");

            builder.Property(u => u.SightSeenId).HasColumnName("sight_seen_id");
            builder.HasKey(u => u.SightSeenId);

            builder.Property(u => u.SightId).HasColumnName("sight_id");
            builder.Property(u => u.VisitId).HasColumnName("visit_id");
        }
    }
}
