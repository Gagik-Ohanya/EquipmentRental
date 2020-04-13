using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.BLL.Models;
using EquipmentRental.BLL.Models.Request;
using EquipmentRental.BLL.Models.Response;
using EquipmentRental.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet("get")]
        public async Task<ActionResult<ServiceResult>> GetAsync()
        {
            List<CustomerModel> customers = (await _customerService.GetAsync()).ToList();

            return new ServiceResult
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success",
                Data = customers
            };
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResult>> CreateAsync(CustomerRequestModel requestModel)
        {
            #region Input Validation
            CustomerModel customer = (await _customerService.GetAsync(c => c.Email == requestModel.Email)).FirstOrDefault();
            if (customer != null)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Customer with email {customer.Email} already exists"
                };
            }
            #endregion

            customer = _mapper.Map<CustomerModel>(requestModel);
            customer = await _customerService.AddWithSaveAsync(customer);

            return new ServiceResult
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success",
                Data = customer
            };
        }

        [HttpPut("update")]
        public async Task<ActionResult<ServiceResult>> UpdateAsync(CustomerRequestModel requestModel)
        {
            #region Input Validation
            CustomerModel customer = (await _customerService.GetAsync(c => c.Id == requestModel.Id)).FirstOrDefault();
            if (customer is null)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Customer not found"
                };
            }
            bool customerWithEmailExists = await _customerService.AnyAsync(c => c.Id != requestModel.Id && c.Email == requestModel.Email);
            if (customerWithEmailExists)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Customer with email {requestModel.Email} already exists"
                };
            }
            #endregion

            customer = _mapper.Map<CustomerModel>(requestModel);
            _customerService.Update(customer);
            await _customerService.SaveChangesAsync();

            return new ServiceResult
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success",
                Data = customer
            };
        }
    }
}