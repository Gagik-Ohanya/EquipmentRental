using EquipmentRental.DAL.Entities;
using EquipmentRental.DAL.Repositories.Interfaces;

namespace EquipmentRental.DAL.Repositories
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(EquipmentRentalDbContext equipmentRentalDbContext) 
            : base(equipmentRentalDbContext)
        {
        }
    }
}