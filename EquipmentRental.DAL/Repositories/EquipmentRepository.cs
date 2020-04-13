using EquipmentRental.DAL.Entities;
using EquipmentRental.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentRental.DAL.Repositories
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(EquipmentRentalDbContext equipmentRentalDbContext)
            : base(equipmentRentalDbContext)
        {
        }

        public async Task<List<Equipment>> GetWithTypeAsync()
        {
            return await DbSet.Include(e => e.EquipmentType).AsNoTracking().ToListAsync();
        }

        public async Task<List<Equipment>> GetWithTypeAsync(Expression<Func<Equipment, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException("Input predicate is null");

            return await DbSet.Include(e => e.EquipmentType).Where(predicate).AsNoTracking().ToListAsync();
        }
    }
}