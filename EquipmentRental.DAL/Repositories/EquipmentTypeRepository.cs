using EquipmentRental.DAL.Entities;
using EquipmentRental.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentRental.DAL.Repositories
{
    public class EquipmentTypeRepository : Repository<EquipmentType>, IEquipmentTypeRepository
    {
        public EquipmentTypeRepository(EquipmentRentalDbContext equipmentRentalDbContext) 
            : base(equipmentRentalDbContext)
        {
        }
    }
}