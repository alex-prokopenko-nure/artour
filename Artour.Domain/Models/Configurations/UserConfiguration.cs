using Artour.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(u => u.UserId).HasColumnName("user_id");
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Username).HasColumnName("username");
            builder.Property(u => u.DateOfBirth).HasColumnName("date_of_birth");
            builder.Property(u => u.Email).HasColumnName("email");
            builder.Property(u => u.FirstName).HasColumnName("first_name");
            builder.Property(u => u.LastName).HasColumnName("last_name");
            builder.Property(u => u.Password).HasColumnName("password");
            builder.Property(u => u.ProfileType).HasColumnName("profile_type").HasDefaultValue(ProfileType.Common);
        }
    }
}
