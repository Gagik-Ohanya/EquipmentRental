using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentRental.BLL.Services.Interfaces
{
    public interface IService<TModel, TEntity> 
        where TModel : class 
        where TEntity : class
    {
        Task<List<TModel>> GetAsync();
        Task<List<TModel>> GetAsync(Expression<Func<TModel, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TModel, bool>> predicate);

        Task<TModel> AddAsync(TModel model);
        Task<TModel> AddWithSaveAsync(TModel model);
        void Update(TModel model);

        void Remove(TModel model);

        Task<int> SaveChangesAsync();
    }
}