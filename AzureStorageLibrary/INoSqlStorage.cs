using System.Linq.Expressions;

namespace AzureStorageLibrary;
public interface INoSqlStorage<TEntity>
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task RemoveAsync(string rowKey,string partitionKey);
    Task<TEntity> GetAsync(string rowKey,string partitionKey);
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity,bool>> query);
}
