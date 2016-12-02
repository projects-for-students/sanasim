using Microsoft.EntityFrameworkCore;
using Sana.Sim.Business.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Sana.Sim.EF.DbEntities;

namespace Sana.Sim.EF.DbAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<DbFeature> Features { get; set; }

        public DbSet<DbProject> Projects { get; set; }

        public DbSet<DbWebshop> Webshops { get; set; }

        public DbSet<DbServer> Servers { get; set; }

        public DbSet<DbDeveloper> Developers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbFeature>().ToTable("Features");
            modelBuilder.Entity<DbProject>().ToTable("Projects");
            modelBuilder.Entity<DbDeveloper>().ToTable("Developers");
            modelBuilder.Entity<DbServer>().ToTable("Servers");
            modelBuilder.Entity<DbWebshop>().ToTable("Webshops");

            modelBuilder.Entity<DbProjectChangeSet>().ToTable("ProjectChangeSets");
            modelBuilder.Entity<DbWebshopChangeSet>().ToTable("WebshopChangeSets");

            modelBuilder.Entity<DbWebshopFeature>().ToTable("WebshopFeatures");
            modelBuilder.Entity<DbProjectDeveloper>().ToTable("ProjectDevelopers");
            modelBuilder.Entity<DbProjectServer>().ToTable("ProjectServers");
        }
    }
}
