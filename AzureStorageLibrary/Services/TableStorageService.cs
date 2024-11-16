using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace AzureStorageLibrary.Services;
public class TableStorageService<TEntity> : INoSqlStorage<TEntity>
    where TEntity : class, ITableEntity, new()
{
    private readonly TableClient _tableClient;
    public TableStorageService()
    {
        var connectionString = ConnectionStrings.AzureStorageConnectionString;
        var tableName = typeof(TEntity).Name + 's';
        var serviceClient = new TableServiceClient(connectionString);
        _tableClient = serviceClient.GetTableClient(tableName);
        _tableClient.CreateIfNotExists();
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.RowKey = Guid.NewGuid().ToString();
        await _tableClient.AddEntityAsync(entity);
        return await GetAsync(entity.RowKey,entity.PartitionKey);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        var entities = new List<TEntity>();
        var result =  _tableClient.QueryAsync<TEntity>();
        await foreach (var row in result)
            entities.Add(row);
        return entities.AsQueryable();
    }

    public async Task<TEntity> GetAsync(string rowKey, string partitionKey)
    {
        try
        {
            var response = await _tableClient.GetEntityAsync<TEntity>(rowKey, partitionKey);
            return response.Value;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return default;
        }
    }

    public async Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> query)
    {
        var entities = new List<TEntity>();
        AsyncPageable<TEntity> result = _tableClient.QueryAsync<TEntity>(query);
        await foreach (var row in result)
            entities.Add(row);
        return entities.AsQueryable();
    }

    public async Task RemoveAsync(string rowKey, string partitionKey)
    {
        await _tableClient.DeleteEntityAsync(rowKey, partitionKey);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await _tableClient.UpdateEntityAsync(entity,entity.ETag,TableUpdateMode.Replace);
        return await GetAsync(entity.RowKey, entity.PartitionKey);
    }
}
