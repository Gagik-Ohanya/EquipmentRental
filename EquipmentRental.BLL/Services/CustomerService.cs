using AutoMapper;
using EquipmentRental.BLL.Models;
using EquipmentRental.BLL.Services.Interfaces;
using EquipmentRental.DAL.Entities;
using EquipmentRental.DAL.Repositories.Interfaces;

namespace EquipmentRental.BLL.Services
{
    public class CustomerService : Service<CustomerModel, Customer>, ICustomerService
    {
        public CustomerService(IMapper mapper, ICustomerRepository customerRepository) 
            : base(mapper, customerRepository)
        {
        }
    }
}