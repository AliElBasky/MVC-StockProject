using CodeZoneStock.Models.CodeZoneStockDbContext;
using CodeZoneStock.Models.DataEntities;
using CodeZoneStock.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeZoneStock.Controllers
{
    public class StoresController : Controller
    {
        private readonly CodeZoneStockDbContext _context;

        public StoresController(CodeZoneStockDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var StoresData = await _context.Stores.ToListAsync();
            var Stores = new StoresViewModel();
            Stores.Stores = StoresData;
            return View(Stores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StoresViewModel model)
        {
            if (!ModelState.IsValid || model.Name.Length < 3)
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    ModelState.AddModelError("Name", "Name Cannot be empty!");
                }else if (model.Name.Length < 2)
                {
                    ModelState.AddModelError("Name", "Name should be 2 charactars at least!");
                }
                return RedirectToAction("index", model);
            }

            Store store = new Store();
            store.Name = model.Name;
            await _context.Stores.AddAsync(store);
            await _context.SaveChangesAsync();

            model.Stores = await _context.Stores.ToListAsync();
            return RedirectToAction("Index", model);
        }
    }
}
