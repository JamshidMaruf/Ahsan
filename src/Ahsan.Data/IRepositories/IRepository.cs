using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ahsan.Data.IRepositories
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true);
        ValueTask<TEntity> InsertAsync(TEntity entity);
        ValueTask<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);
        ValueTask<TEntity> UpdateAsync(TEntity entity);
        ValueTask<bool> DeleteAsync(TEntity entity);
        ValueTask SaveChangesAsync();
    }
}
