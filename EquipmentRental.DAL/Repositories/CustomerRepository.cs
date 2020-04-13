using EquipmentRental.DAL.Entities;
using EquipmentRental.DAL.Repositories.Interfaces;

namespace EquipmentRental.DAL.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(EquipmentRentalDbContext equipmentRentalDbContext) 
            : base(equipmentRentalDbContext)
        {
        }
    }
}