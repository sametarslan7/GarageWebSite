using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GarageWebSite.Models.Classes
{
    public class Context : DbContext
    {
        // Veritabanı tablolarını temsil eden DbSet'ler
        public DbSet<Car> Cars { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Car-Photo One-to-Many Relationship
            modelBuilder.Entity<Photo>()
                .HasRequired(p => p.Car)
                .WithMany(c => c.Photos)
                .HasForeignKey(p => p.CarId);

            // Car-Brand One-to-Many Relationship
            modelBuilder.Entity<Car>()
                .HasRequired(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BrandId);

            // Car-FuelType One-to-Many Relationship
            modelBuilder.Entity<Car>()
                .HasRequired(c => c.FuelType)
                .WithMany(f => f.Cars)
                .HasForeignKey(c => c.FuelId);
        }
    }
}