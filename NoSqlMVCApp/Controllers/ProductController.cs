using AzureStorageLibrary;
using AzureStorageLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoSqlMVCApp.Models;

namespace NoSqlMVCApp.Controllers;
public class ProductController : Controller
{
    private readonly INoSqlStorage<Product> noSqlStorage;
    private readonly INoSqlStorage<Store> noSqlStorageStores;

    public ProductController(INoSqlStorage<Product> noSqlStorage, INoSqlStorage<Store> noSqlStorageStores)
    {
        this.noSqlStorage = noSqlStorage;
        this.noSqlStorageStores = noSqlStorageStores;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var allProducts = await noSqlStorage.GetAllAsync();
        var productModel = new ProductViewModel { Products = allProducts.ToList() };
        return View(productModel);
    }

    [HttpGet]
    public async Task<IActionResult> AddProduct()
    {
        var stores = await noSqlStorageStores.GetAllAsync();
        var storeNameList = new List<string>();
        foreach (var store in stores)
        {
            storeNameList.Add(store.Name);
        }

        ViewBag.Stores = new SelectList(storeNameList);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddProduct(AddProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var newProduct = new Product
            {
                Name = model.Name,
                Stock = model.Stock,
                Color = model.Color,
                PartitionKey = model.PartitionKey,
                Price = model.Price,
            };
            var stores = await noSqlStorageStores.QueryAsync(s => s.Name == model.SelectedStore);
            newProduct.StoreKey = stores.FirstOrDefault(s=>s.Name == model.SelectedStore).RowKey;
            await noSqlStorage.AddAsync(newProduct);
            return RedirectToAction("Index");
        }
        ModelState.AddModelError("", "Can not add the product !");
        return View(model);
    }
}
