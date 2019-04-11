using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comment");

            builder.Property(x => x.CommentId).HasColumnName("comment_id");
            builder.HasKey(x => x.CommentId);

            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.TourId).HasColumnName("tour_id");
            builder.Property(x => x.Mark).HasColumnName("mark");
            builder.Property(x => x.Text).HasColumnName("text");
        }
    }
}
