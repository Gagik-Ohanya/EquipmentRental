using EquipmentRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRental.DAL.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentType>().HasData(
                new EquipmentType { Id = 1, Name = "Heavy", LoyaltyPoints = 2 },
                new EquipmentType { Id = 2, Name = "Regular", LoyaltyPoints = 1 },
                new EquipmentType { Id = 3, Name = "Specialized", LoyaltyPoints = 1 }
            );
            modelBuilder.Entity<RentalType>().HasData(
                new RentalType { Id = 1, Name = "One-time", Fee = 100 },
                new RentalType { Id = 2, Name = "Premium", Fee = 60 },
                new RentalType { Id = 3, Name = "Regular", Fee = 40 }
            );
        }
    }
}