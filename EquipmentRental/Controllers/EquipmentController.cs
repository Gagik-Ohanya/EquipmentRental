using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.BLL.Enums;
using EquipmentRental.BLL.Facades.Interfaces;
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
        private readonly IEquipmentFacade _equipmentFacade;
        private readonly IMapper _mapper;

        public EquipmentController(
            IEquipmentService equipmentService, 
            IEquipmentFacade equipmentFacade, 
            IMapper mapper)
        {
            _equipmentService = equipmentService;
            _equipmentFacade = equipmentFacade;
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
            return await _equipmentFacade.RentAsync(requestModel);
        }

        [HttpPut("return/{id}")]
        public async Task<ActionResult<ServiceResult>> ReturnAsync(int id)
        {
            return await _equipmentFacade.ReturnAsync(id);
        }
    }
}