using Ahsan.Data.Contexts;
using Ahsan.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

#pragma warning disable

namespace Ahsan.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext dbContext;
    protected readonly DbSet<TEntity> dbSet;
    public Repository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<TEntity>();
    }

    public async ValueTask<TEntity> InsertAsync(TEntity entity)
        => (await dbSet.AddAsync(entity)).Entity;

    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        EntityEntry<TEntity> entryentity = this.dbContext.Update(entity);
        return entryentity.Entity;
    }

    public async ValueTask<bool> DeleteAsync(TEntity entity)
    {
        dbSet.Remove(entity);
        return true;
    }

    public IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true)
    {
        IQueryable<TEntity> query = expression is null ? dbSet : dbSet.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public async ValueTask<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
        => await GetAll(expression, includes).FirstOrDefaultAsync();

    public async ValueTask SaveChangesAsync()
        => await dbContext.SaveChangesAsync();
}
