using EquipmentRental.BLL.Models.Request;
using EquipmentRental.BLL.Models.Response;
using System.Threading.Tasks;

namespace EquipmentRental.BLL.Facades.Interfaces
{
    public interface IEquipmentFacade
    {
        Task<ServiceResult> RentAsync(RentingRequestModel requestModel);
        Task<ServiceResult> ReturnAsync(int rentalId);
    }
}