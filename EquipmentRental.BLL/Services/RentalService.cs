using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.BLL.Enums;
using EquipmentRental.BLL.Models;
using EquipmentRental.BLL.Services.Interfaces;
using EquipmentRental.DAL.Entities;
using EquipmentRental.DAL.Repositories.Interfaces;

namespace EquipmentRental.BLL.Services
{
    public class RentalService : Service<RentalModel, Rental>, IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IRentalTypeRepository _rentalTypeRepository;

        public RentalService(
                IMapper mapper,
                IRentalRepository rentalRepository,
                IRentalTypeRepository rentalTypeRepository)
            : base(mapper, rentalRepository)
        {
            _rentalRepository = rentalRepository;
            _rentalTypeRepository = rentalTypeRepository;
        }

        public async Task<RentalModel> RentAsync(RentalModel rentalModel, int equipmentTypeId)
        {
            var rentalTypes = await _rentalTypeRepository.GetAsync();
            int rentalDays = (rentalModel.EndDate - rentalModel.StartDate).Days;
            decimal oneTimeFee = rentalTypes.Where(rt => rt.Id == (int)RentalTypes.OneTime).First().Fee;
            decimal premiumFee = rentalTypes.Where(rt => rt.Id == (int)RentalTypes.Premium).First().Fee;
            decimal regularFee = rentalTypes.Where(rt => rt.Id == (int)RentalTypes.Regular).First().Fee;
            
            rentalModel.Status = (int)RentalStatus.InProgress;
            switch (equipmentTypeId)
            {
                case (int)EquipmentTypes.Heavy:
                    rentalModel.Fee = oneTimeFee + rentalDays * premiumFee;
                    break;
                case (int)EquipmentTypes.Regular:
                    rentalModel.Fee = oneTimeFee + Math.Min(2, rentalDays) * premiumFee + Math.Max(0, rentalDays - 2) * regularFee;
                    break;
                case (int)EquipmentTypes.Specialized:
                    rentalModel.Fee = premiumFee * Math.Min(3, rentalDays) + regularFee * Math.Max(0, rentalDays - 3);
                    break;
                default:
                    break;
            }
            Rental rental = Mapper.Map<Rental>(rentalModel);
            _rentalRepository.Update(rental);
            await _rentalRepository.SaveChangesAsync();

            return rentalModel;
        }

        public async Task<RentalModel> ReturnAsync(RentalModel rentalModel)
        {
            rentalModel.ReturnDate = DateTime.Now;
            rentalModel.Status = rentalModel.EndDate > rentalModel.ReturnDate ? (int)RentalStatus.ReturnedLate : (int)RentalStatus.ReturnedInTime;
            Rental rental = Mapper.Map<Rental>(rentalModel);
            _rentalRepository.Update(rental);
            await _rentalRepository.SaveChangesAsync();

            return rentalModel;
        }
    }
}