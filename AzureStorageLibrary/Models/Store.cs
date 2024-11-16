using Azure;
using Azure.Data.Tables;

namespace AzureStorageLibrary.Models;
public class Store : ITableEntity
{
    public string PartitionKey { get; set;}
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string? Name { get; set; }
    public string? CountryName { get; set; }
    public string? CityName { get; set; }
    public string? Address {  get; set; }
}
