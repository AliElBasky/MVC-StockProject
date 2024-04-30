using CodeZoneStock.Models.CodeZoneStockDbContext;
using CodeZoneStock.Models.DataEntities;
using CodeZoneStock.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace CodeZoneStock.Controllers
{
    public class StoresController : Controller
    {
        private readonly CodeZoneStockDbContext _context;
        private readonly IToastNotification _toastNotification;

        public StoresController(CodeZoneStockDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var Stores = await _context.Stores.Select(x=> new StoreFormViewModel {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            //var Stores = new List<StoreFormViewModel>();
            //foreach (var item in StoresData)
            //{
            //    var store = new StoreFormViewModel();
                
            //    store.Id = item.Id;
            //    store.Name = item.Name;
            //    Stores.Add(store);
            //}
            return View(Stores);
        }

        public async Task<IActionResult> CreateStore()
        {
            StoreFormViewModel viewModel = new StoreFormViewModel();
            return View("StoreForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StoreFormViewModel model)
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
                return View("StoreForm", model);
            }

            Store store = new Store();
            store.Name = model.Name;
            await _context.Stores.AddAsync(store);
            await _context.SaveChangesAsync();

            _toastNotification.AddSuccessToastMessage("Store Created Successfully");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                return BadRequest();

            var Store = await _context.Stores.FindAsync(id);


            if (Store == null)
                return NotFound();

            var ViewModel = new StoreFormViewModel();
            ViewModel.Id = Store.Id;
            ViewModel.Name = Store.Name;

            return View("StoreForm", ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StoreFormViewModel model)
        {
            if (!ModelState.IsValid || model.Name.Length < 3)
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    ModelState.AddModelError("Name", "Name Cannot be empty!");
                }
                else if (model.Name.Length < 2)
                {
                    ModelState.AddModelError("Name", "Name should be 2 charactars at least!");
                }
                return RedirectToAction("index", model);
            }

            var Store = await _context.Stores.FindAsync(model.Id);

            if (Store == null)
                return NotFound();

            Store.Name = model.Name;
            await _context.SaveChangesAsync();

            _toastNotification.AddSuccessToastMessage("Store Updated Successfully");
            return RedirectToAction("index", model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var Store = await _context.Stores.FindAsync(id);

            if (Store == null)
                return NotFound();

            _context.Stores.Remove(Store);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
