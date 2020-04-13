using AutoMapper;
using EquipmentRental.BLL.Services.Interfaces;
using EquipmentRental.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EquipmentRental.BLL.Services
{
    public abstract class Service<TModel, TEntity> : IService<TModel, TEntity>
        where TModel : class
        where TEntity : class
    {
        protected IMapper Mapper;
        protected IRepository<TEntity> Repository;

        protected Service(IMapper mapper, IRepository<TEntity> repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public virtual async Task<TModel> AddAsync(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException("Input model is null");

            TEntity entity = Mapper.Map<TEntity>(model);
            entity = await Repository.AddAsync(entity);

            return Mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> AddWithSaveAsync(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException("Input model is null");

            TEntity entity = Mapper.Map<TEntity>(model);
            entity = await Repository.AddAsync(entity);
            await Repository.SaveChangesAsync();

            return Mapper.Map<TModel>(entity);
        }

        public virtual async Task<List<TModel>> GetAsync()
        {
            IEnumerable<TEntity> entities = await Repository.GetAsync();
            return Mapper.Map<List<TModel>>(entities);
        }

        public async Task<List<TModel>> GetAsync(Expression<Func<TModel, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException("Predicate is null");

            var entityPredicate = Mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            IEnumerable<TEntity> entities = await Repository.GetAsync(entityPredicate);

            return Mapper.Map<List<TModel>>(entities);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TModel, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException("Predicate is null");

            var entityPredicate = Mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            return await Repository.AnyAsync(entityPredicate);
        }

        public void Update(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException("Input model is null");

            TEntity entity = Mapper.Map<TEntity>(model);
            Repository.Update(entity);
        }

        public void Remove(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException("Input model is null");

            TEntity entity = Mapper.Map<TEntity>(model);
            Repository.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Repository.SaveChangesAsync();
        }
    }
}