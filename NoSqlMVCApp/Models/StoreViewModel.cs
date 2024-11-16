using AzureStorageLibrary.Models;

namespace NoSqlMVCApp.Models;
public class StoreViewModel
{
    public IEnumerable<Store>? Stores { get; set; }
}
