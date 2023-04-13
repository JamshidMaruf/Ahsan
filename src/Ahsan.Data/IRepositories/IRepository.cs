using System.Linq.Expressions;

namespace Ahsan.Data.IRepositories
{
    public interface IRepository<TEntity>
    {
        ValueTask<TEntity> InsertAsync(TEntity entity);
        ValueTask<TEntity> UpdateAsync(TEntity entity);
        ValueTask<bool> DeleteAsync(TEntity entity);
        ValueTask SaveChangesAsync();
        IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true);
        ValueTask<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);
    }
}
