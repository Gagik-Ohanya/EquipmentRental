using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.BLL.Enums;
using EquipmentRental.BLL.Models;
using EquipmentRental.BLL.Models.Request;
using EquipmentRental.BLL.Models.Response;
using EquipmentRental.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        private readonly ICustomerService _customerService;
        private readonly IRentalService _rentalService;
        private readonly IMapper _mapper;

        public EquipmentController(
              IEquipmentService equipmentService
            , ICustomerService customerService
            , IRentalService rentalService
            , IMapper mapper)
        {
            _equipmentService = equipmentService;
            _customerService = customerService;
            _rentalService = rentalService;
            _mapper = mapper;
        }

        [HttpGet("get")]
        public async Task<ActionResult<ServiceResult>> GetAsync()
        {
            List<EquipmentResponseModel> equipments = (await _equipmentService.GetWithTypeAsync()).ToList();

            return new ServiceResult
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success",
                Data = equipments
            };
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResult>> CreateAsync(EquipmentRequestModel requestModel)
        {
            #region Input Validation
            EquipmentModel equipment = (await _equipmentService.GetAsync(e => e.Name == requestModel.Name)).FirstOrDefault();
            if (equipment != null)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Equipment with name {equipment.Name} already exists"
                };
            }
            #endregion

            equipment = _mapper.Map<EquipmentModel>(requestModel);
            equipment = await _equipmentService.AddWithSaveAsync(equipment);

            return new ServiceResult
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success",
                Data = equipment
            };
        }

        [HttpPut("update")]
        public async Task<ActionResult<ServiceResult>> UpdateAsync(EquipmentRequestModel requestModel)
        {
            #region Input Validation
            EquipmentModel equipment = (await _equipmentService.GetAsync(e => e.Id == requestModel.Id)).FirstOrDefault();
            if (equipment is null)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Equipment not found"
                };
            }
            bool equipmentWithNameExists = await _equipmentService.AnyAsync(c => c.Id != requestModel.Id && c.Name == requestModel.Name);
            if (equipmentWithNameExists)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Equipment with name {requestModel.Name} already exists"
                };
            }
            #endregion

            equipment = _mapper.Map<EquipmentModel>(requestModel);
            _equipmentService.Update(equipment);
            await _equipmentService.SaveChangesAsync();

            return new ServiceResult
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success",
                Data = equipment
            };
        }

        [HttpPost("rent")]
        public async Task<ActionResult<ServiceResult>> RentAsync(RentingRequestModel requestModel)
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

        [HttpPut("return/{id}")]
        public async Task<ActionResult<ServiceResult>> ReturnAsync(int id)
        {
            #region Input Validation
            var rentalModel = (await _rentalService.GetAsync(r => r.Id == id && r.Status == (int)RentalStatus.InProgress)).FirstOrDefault();
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