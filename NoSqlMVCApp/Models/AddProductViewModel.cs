using AzureStorageLibrary;
using AzureStorageLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace NoSqlMVCApp.Models;
public class AddProductViewModel
{
    [Required]
    public string PartitionKey { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public int Stock { get; set; }
    public string? Color { get; set; }
    public string? SelectedStore { get; set; }
}
