using AzureStorageLibrary.Models;

namespace NoSqlMVCApp.Models;
public class ProductViewModel
{
    public IEnumerable<Product>? Products { get; set; }
}
