using Distributors.Core.Repositories;
using Distributors.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Distributors.Infrastructure.Repositories;
public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
    private DistributorsDbContext DbContext { get; }
    protected DbSet<TEntity> Set => DbContext.Set<TEntity>();

    protected GenericRepository(DistributorsDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public void Delete(TEntity entity)
    {
        Set.Remove(entity);
    }

    public async Task<bool> ExistsByPrimaryKeyAsync(params object?[]? primaryKeyValues)
    {
        return await Set.FindAsync(primaryKeyValues) is not null;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Set.ToListAsync();
    }

    public async Task<TEntity?> GetByPrimaryKeyAsync(params object?[]? primaryKeyValues)
    {
        return await Set.FindAsync(primaryKeyValues);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await Set.AddAsync(entity);
    }
}
