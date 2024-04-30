using CodeZoneStock.Models.CodeZoneStockDbContext;
using CodeZoneStock.Models.DataEntities;
using CodeZoneStock.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace CodeZoneStock.Controllers
{
    public class StockController : Controller
    {
        private readonly CodeZoneStockDbContext _context;
        private readonly IToastNotification _toastNotification;

        public StockController(CodeZoneStockDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var Items = await _context.Items.Select(x=> new ItemFormViewModel {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            var Stores = await _context.Stores.Select(x => new StoreFormViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            StockFormViewModel Stock = new StockFormViewModel();
            Stock.Stores = Stores;
            Stock.Items = Items;

            return View(Stock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StockFormViewModel model)
        {
            if (!ModelState.IsValid || model.Quantity < 1)
            {
                if (model.Quantity < 1)
                {
                    ModelState.AddModelError("Quantity", "Quantity should be at least 1");
                }
                return RedirectToAction("Add", model);
            }
            var store = await _context.Stores.Include(s => s.ItemStores)
                    .FirstOrDefaultAsync(s => s.Id == model.StoreId);

            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == model.ItemId);

            if (store != null && item != null)
            {
                // Check if the item is already associated with the store
                var storeItem = store.ItemStores.FirstOrDefault(si => si.ItemId == item.Id);

                if (storeItem != null)
                {
                    // Update the quantity if it's already associated
                    storeItem.Quantity += model.Quantity;
                    model.TotalQuantity = storeItem.Quantity;
                }
                else
                {
                    // Create a new StoreItem and add it if not already associated
                    store.ItemStores.Add(new StoreItem
                    {
                        Item = item,
                        Quantity = model.Quantity
                    });
                    model.TotalQuantity = model.Quantity;
                }

                // Save the changes in the database
                await _context.SaveChangesAsync();
            }

            _toastNotification.AddSuccessToastMessage("Store Updated Successfully");
            return RedirectToAction("Index");
            //return Ok();
        }
    }
}
