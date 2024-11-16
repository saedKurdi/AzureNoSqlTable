using System.ComponentModel.DataAnnotations;

namespace NoSqlMVCApp.Models;
public class AddStoreViewModel
{
    [Required]
    public string? StoreName { get; set; }
    [Required]
    public string PartitionKey { get; set; }
    [Required]
    public string? CountryName { get; set; }
    [Required]
    public string? CityName { get; set; }
    [Required]
    public string? Address { get; set; }
}
