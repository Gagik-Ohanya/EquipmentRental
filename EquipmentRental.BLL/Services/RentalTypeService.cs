using AutoMapper;
using EquipmentRental.BLL.Models;
using EquipmentRental.BLL.Services.Interfaces;
using EquipmentRental.DAL.Entities;
using EquipmentRental.DAL.Repositories.Interfaces;

namespace EquipmentRental.BLL.Services
{
    public class RentalTypeService : Service<RentalTypeModel, RentalType>, IRentalTypeService
    {
        public RentalTypeService(IMapper mapper, IRentalTypeRepository rentalTypeRepository) 
            : base(mapper, rentalTypeRepository)
        {
        }
    }
}