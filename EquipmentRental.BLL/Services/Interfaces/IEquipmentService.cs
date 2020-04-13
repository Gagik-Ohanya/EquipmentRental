using EquipmentRental.BLL.Models;
using EquipmentRental.BLL.Models.Response;
using EquipmentRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EquipmentRental.BLL.Services.Interfaces
{
    public interface IEquipmentService : IService<EquipmentModel, Equipment>
    {
        Task<List<EquipmentResponseModel>> GetWithTypeAsync(Expression<Func<EquipmentModel, bool>> predicate = null);
    }
}