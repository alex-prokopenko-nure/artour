using Artour.Domain.Constants;
using Artour.Domain.Models;
using Artour.Domain.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.EntityFramework.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Sight> Sights { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<SightImage> SightImages { get; set; }
        public virtual DbSet<SightSeen> SightSeens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionStrings.DatabaseConnectionString, o => o.CommandTimeout(60 * 10));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TourConfiguration());
            modelBuilder.ApplyConfiguration(new VisitConfiguration());
            modelBuilder.ApplyConfiguration(new SightConfiguration());
            modelBuilder.ApplyConfiguration(new SightImageConfiguration());
            modelBuilder.ApplyConfiguration(new SightSeenConfiguration());
        }
    }
}
