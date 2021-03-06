﻿// <auto-generated />
using System;
using Artour.Domain.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Artour.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190318143600_User")]
    partial class User
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Artour.Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_id");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.ToTable("user");
                });
#pragma warning restore 612, 618
        }
    }
}
