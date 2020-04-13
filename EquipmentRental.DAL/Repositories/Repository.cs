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
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> DbSet;

        protected Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException("Input entity is null");

            return (await DbSet.AddAsync(entity)).Entity;
        }

        public virtual async Task<List<T>> GetAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException("Input predicate is null");

            return await DbSet.Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException("Input predicate is null");

            return await DbSet.AnyAsync(predicate);
        }

        public virtual void Update(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException("Input entity is null");

            DbSet.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException("Input entity is null");

            DbSet.Remove(entity);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}