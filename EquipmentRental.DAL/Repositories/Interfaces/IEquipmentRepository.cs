using EquipmentRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EquipmentRental.DAL.Repositories.Interfaces
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        Task<List<Equipment>> GetWithTypeAsync();
        Task<List<Equipment>> GetWithTypeAsync(Expression<Func<Equipment, bool>> predicate = null);
    }
}