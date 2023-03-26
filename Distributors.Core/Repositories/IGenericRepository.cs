namespace Distributors.Core.Repositories;
public interface IGenericRepository<TEntity>
    where TEntity : class
{
    Task InsertAsync(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity?> GetByPrimaryKeyAsync(params object?[]? primaryKeyValues);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<bool> ExistsByPrimaryKeyAsync(params object?[]? primaryKeyValues);
}
