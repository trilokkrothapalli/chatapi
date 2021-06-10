using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chat.Data.Models.Interface;

namespace Chat.Data.GenericRepository.Interface
{
    public interface IApplicationRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(int id);
    }
}
