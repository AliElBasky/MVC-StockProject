using CodeZoneStock.Models.CodeZoneStockDbContext;
using CodeZoneStock.Models.DataEntities;
using CodeZoneStock.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace CodeZoneStock.Controllers
{
    public class ItemsController : Controller
    {
        private readonly CodeZoneStockDbContext _context;
        private readonly IToastNotification _toastNotification;

        public ItemsController(CodeZoneStockDbContext context, IToastNotification toastNotification)
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
            
            return View(Items);
        }

        public async Task<IActionResult> CreateItem()
        {
            ItemFormViewModel viewModel = new ItemFormViewModel();
            return View("ItemForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ItemFormViewModel model)
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
                return View("ItemForm", model);
            }

            Item Item = new Item();
            Item.Name = model.Name;
            await _context.Items.AddAsync(Item);
            await _context.SaveChangesAsync();

            _toastNotification.AddSuccessToastMessage("Item Created Successfully");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                return BadRequest();

            var Item = await _context.Items.FindAsync(id);


            if (Item == null)
                return NotFound();

            var ViewModel = new ItemFormViewModel();
            ViewModel.Id = Item.Id;
            ViewModel.Name = Item.Name;

            return View("ItemForm", ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemFormViewModel model)
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

            var Item = await _context.Items.FindAsync(model.Id);

            if (Item == null)
                return NotFound();

            Item.Name = model.Name;
            await _context.SaveChangesAsync();

            _toastNotification.AddSuccessToastMessage("Item Updated Successfully");
            return RedirectToAction("index", model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var Item = await _context.Items.FindAsync(id);

            if (Item == null)
                return NotFound();

            _context.Items.Remove(Item);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
