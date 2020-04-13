using AutoMapper;
using EquipmentRental.BLL.Models;
using EquipmentRental.BLL.Models.Request;
using EquipmentRental.BLL.Models.Response;
using EquipmentRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentRental.BLL.AutoMapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<Equipment, EquipmentModel>().ReverseMap();
            CreateMap<EquipmentType, EquipmentTypeModel>().ReverseMap();
            CreateMap<Rental, RentalModel>().ReverseMap();
            CreateMap<RentalType, RentalTypeModel>().ReverseMap();

            CreateMap<CustomerRequestModel, CustomerModel>().ReverseMap();
            CreateMap<EquipmentRequestModel, EquipmentModel>().ReverseMap();
            CreateMap<RentingRequestModel, RentalModel>().ReverseMap();
            
            CreateMap<EquipmentResponseModel, EquipmentModel>().ReverseMap();
        }
    }
}