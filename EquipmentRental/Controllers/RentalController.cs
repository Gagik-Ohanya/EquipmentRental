using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.BLL.Models;
using EquipmentRental.BLL.Models.Response;
using EquipmentRental.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalservice;
        private readonly IMapper _mapper;

        public RentalController(IRentalService rentalservice, IMapper mapper)
        {
            _rentalservice = rentalservice;
            _mapper = mapper;
        }

        [HttpGet("invoice/{customerId}")]
        public async Task<ActionResult<ServiceResult>> GetInvoice(int customerId)
        {
            List<RentalModel> rentals = await _rentalservice.GetAsync(r => r.CustomerId == customerId);
            if (rentals is null)
                return new ServiceResult { StatusCode = HttpStatusCode.BadRequest, Message = "No operations done" };

            StringBuilder sb = new StringBuilder("Invoice");
            foreach (var rental in rentals)
            {
                sb.Append($"\n{rental.EquipmentId} - {rental.Fee}");
            }
            sb.Append($"\nSummary: €{rentals.Sum(r => r.Fee)}");
            sb.Append("\n");

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "application/text");
        }
    }
}