using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipmentRental.DAL.Entities.Configurations
{
    public class RentalTypeConfiguration : IEntityTypeConfiguration<RentalType>
    {
        public void Configure(EntityTypeBuilder<RentalType> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar(50)");
            builder.Property(p => p.Fee).HasColumnType("decimal(18,2)");
        }
    }
}