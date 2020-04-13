using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.BLL.Models;
using EquipmentRental.BLL.Models.Response;
using EquipmentRental.BLL.Services.Interfaces;
using EquipmentRental.DAL.Entities;
using EquipmentRental.DAL.Repositories.Interfaces;

namespace EquipmentRental.BLL.Services
{
    public class EquipmentService : Service<EquipmentModel, Equipment>, IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentService(IMapper mapper, IEquipmentRepository equipmentRepository) 
            : base(mapper, equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public async Task<List<EquipmentResponseModel>> GetWithTypeAsync(Expression<Func<EquipmentModel, bool>> predicate = null)
        {
            List<Equipment> result = null;
            if (predicate is null)
            {
                result = await _equipmentRepository.GetWithTypeAsync();
            }
            else
            {
                var entityPredicate = Mapper.Map<Expression<Func<Equipment, bool>>>(predicate);
                result = await _equipmentRepository.GetWithTypeAsync(entityPredicate);
            }

            return result.Select(e => new EquipmentResponseModel
            {
                Id = e.Id,
                Name = e.Name,
                TypeId = e.TypeId,
                TypeName = e.EquipmentType.Name,
                LoyaltyPoints = e.EquipmentType.LoyaltyPoints,
                IsAvailable = e.IsAvailable
            }).ToList();
        }
    }
}