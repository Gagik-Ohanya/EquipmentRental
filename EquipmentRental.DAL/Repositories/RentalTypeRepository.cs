using EquipmentRental.DAL.Entities;
using EquipmentRental.DAL.Repositories.Interfaces;

namespace EquipmentRental.DAL.Repositories
{
    public class RentalTypeRepository : Repository<RentalType>, IRentalTypeRepository
    {
        public RentalTypeRepository(EquipmentRentalDbContext equipmentRentalDbContext) 
            : base(equipmentRentalDbContext)
        {
        }
    }
}