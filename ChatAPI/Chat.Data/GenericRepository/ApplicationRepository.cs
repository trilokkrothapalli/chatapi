using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chat.Data.GenericRepository.Interface;
using Chat.Data.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.GenericRepository
{
    public class ApplicationRepository<TEntity> : IApplicationRepository<TEntity> where TEntity:class, IEntity
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> entitySet;

        public ApplicationRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            entitySet = this.dbContext.Set<TEntity>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            entitySet.Remove(await entitySet.FindAsync(id).ConfigureAwait(false));
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await entitySet.AsNoTracking().Where(filter).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await entitySet.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await entitySet.FindAsync(id).ConfigureAwait(false);
        }
    

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            dbContext.Add<TEntity>(entity);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;

        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.Update<TEntity>(entity);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;  
        }
    }
}
