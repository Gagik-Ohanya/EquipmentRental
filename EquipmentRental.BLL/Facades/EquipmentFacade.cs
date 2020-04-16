using AutoMapper;
using EquipmentRental.BLL.Enums;
using EquipmentRental.BLL.Facades.Interfaces;
using EquipmentRental.BLL.Models;
using EquipmentRental.BLL.Models.Request;
using EquipmentRental.BLL.Models.Response;
using EquipmentRental.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentRental.BLL.Facades
{
    public class EquipmentFacade : IEquipmentFacade
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IRentalService _rentalService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public EquipmentFacade(
                IEquipmentService equipmentService,
                IRentalService rentalService,
                ICustomerService customerService,
                IMapper mapper)
        {
            _equipmentService = equipmentService;
            _rentalService = rentalService;
            _customerService = customerService;
            _mapper = mapper;
        }

        public async Task<ServiceResult> RentAsync(RentingRequestModel requestModel)
        {
            #region Input Validation
            bool customerExists = await _customerService.AnyAsync(c => c.Id == requestModel.CustomerId);
            if (!customerExists)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Customer not found"
                };
            }

            var equipmentWithType = (await _equipmentService.GetWithTypeAsync(e => e.Id == requestModel.EquipmentId && e.IsAvailable)).FirstOrDefault();
            if (equipmentWithType is null)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Equipment not available"
                };
            }
            #endregion

            RentalModel rentalModel = _mapper.Map<RentalModel>(requestModel);
            rentalModel = await _rentalService.RentAsync(rentalModel, equipmentWithType.TypeId);

            EquipmentModel equipment = _mapper.Map<EquipmentModel>(equipmentWithType);
            equipment.IsAvailable = false;
            _equipmentService.Update(equipment);
            await _equipmentService.SaveChangesAsync();

            return new ServiceResult
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success",
                Data = rentalModel
            };
        }

        public async Task<ServiceResult> ReturnAsync(int rentalId)
        {
            #region Input Validation
            var rentalModel = (await _rentalService.GetAsync(r => r.Id == rentalId && r.Status == (int)RentalStatus.InProgress)).FirstOrDefault();
            if (rentalModel is null)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Rental not available"
                };
            }
            #endregion

            rentalModel = await _rentalService.ReturnAsync(rentalModel);
            if (rentalModel.Status == (int)RentalStatus.ReturnedInTime)
            {
                CustomerModel customer = (await _customerService.GetAsync(c => c.Id == rentalModel.CustomerId)).First();
                EquipmentResponseModel equipmentWithType = (await _equipmentService.GetWithTypeAsync(et => et.Id == rentalModel.EquipmentId)).First();

                EquipmentModel equipment = _mapper.Map<EquipmentModel>(equipmentWithType);
                equipment.IsAvailable = true;
                _equipmentService.Update(equipment);
                customer.Loyalty += equipmentWithType.LoyaltyPoints;
                _customerService.Update(customer);
                await _customerService.SaveChangesAsync();
            }

            return new ServiceResult
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success",
                Data = rentalModel
            };
        }
    }
}