using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artour.Domain.Models.Configurations
{
    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.ToTable("visit");

            builder.Property(u => u.VisitId).HasColumnName("visit_id");
            builder.HasKey(u => u.VisitId);

            builder.Property(u => u.UserId).HasColumnName("user_id");
            builder.Property(u => u.TourId).HasColumnName("tour_id");
            builder.Property(u => u.StartDate).HasColumnName("start_date");
            builder.Property(u => u.EndDate).HasColumnName("end_date");
        }
    }
}
