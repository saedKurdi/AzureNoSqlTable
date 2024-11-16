using AzureStorageLibrary;
using AzureStorageLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using NoSqlMVCApp.Models;

namespace NoSqlMVCApp.Controllers;
public class StoreController : Controller
{
    private readonly INoSqlStorage<Store> noSqlStorage;
    public StoreController(INoSqlStorage<Store> noSqlStorage)
    {
            this.noSqlStorage = noSqlStorage;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var allStores = await noSqlStorage.GetAllAsync();
        var storeModel = new StoreViewModel { Stores = allStores.ToList() };
        return View(storeModel);
    }

    [HttpGet] 
    public IActionResult AddStore()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddStore(AddStoreViewModel model)
    {
        if (ModelState.IsValid)
        {
            var newStore = new Store
            {
                Name = model.StoreName,
                Address = model.Address,
                CityName = model.CityName,
                CountryName = model.CountryName,
                PartitionKey = model.PartitionKey,
            };
            await noSqlStorage.AddAsync(newStore);
            return RedirectToAction("Index");
        }
        ModelState.AddModelError("", "Can not add the store !");
        return View(model);
    }
}
