using EquipmentRental.BLL.Models;
using EquipmentRental.DAL.Entities;
using System.Threading.Tasks;

namespace EquipmentRental.BLL.Services.Interfaces
{
    public interface IRentalService : IService<RentalModel, Rental>
    {
        Task<RentalModel> RentAsync(RentalModel rentalModel, int equipmentTypeId);
        Task<RentalModel> ReturnAsync(RentalModel rentalModel);
    }
}