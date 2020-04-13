using EquipmentRental.DAL.Entities.Configurations;
using EquipmentRental.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace EquipmentRental.DAL
{
    public class EquipmentRentalDbContext : DbContext
    {
        public EquipmentRentalDbContext(DbContextOptions<EquipmentRentalDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
            modelBuilder.ApplyConfiguration(new EquipmentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RentalConfiguration());
            modelBuilder.ApplyConfiguration(new RentalTypeConfiguration());

            modelBuilder.Seed();
        }
    }
}