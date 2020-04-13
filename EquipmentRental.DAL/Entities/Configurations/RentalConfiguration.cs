using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipmentRental.DAL.Entities.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.Property(p => p.StartDate).HasColumnType("date");
            builder.Property(p => p.EndDate).HasColumnType("date");
            builder.Property(p => p.ReturnDate).HasColumnType("date");
            builder.Property(p => p.Fee).HasColumnType("decimal(18,2)");

            builder.HasOne(rh => rh.Customer)
                .WithMany(c => c.RentalHistory)
                .HasForeignKey(rh => rh.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rh => rh.Equipment)
                .WithMany(c => c.RentalHistory)
                .HasForeignKey(rh => rh.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}