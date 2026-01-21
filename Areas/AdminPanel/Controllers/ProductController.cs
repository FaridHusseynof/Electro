using Electro.Areas.AdminPanel.ViewModels;
using Electro.Data;
using Electro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Electro.Areas.AdminPanel.Controllers
{
    [Authorize(Roles ="Admin, SuperAdmin")]
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private ElectroDbContext _context { get; }
        public ProductController(ElectroDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.products.Where(c => !c.IsDeleted));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM vm)
        {
            if (!ModelState.IsValid) return View();
            Product product = new Product
            {
                Title = vm.title,
                Price = vm.price,
                Category = vm.category
            };

            if (vm.imageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.imageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.imageFile.CopyToAsync(stream);
                }
                product.ImageURL = fileName;
            }

            _context.products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Product? product = await _context.products.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);
            if (product == null) return NotFound();
            product.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Product? product = _context.products.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == id);
            if (product == null) return NotFound();
            UpdateVM vm = new UpdateVM
            {
                id_ = product.Id,
                title = product.Title,
                price = product.Price,
                category = product.Category
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateVM vm) 
        {
            if (!ModelState.IsValid) return View(vm);
            Product? product = await _context.products.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == vm.id_);
            if (product == null) return NotFound();

            product.Title = vm.title;
            product.Price = vm.price;
            product.Category = vm.category;

            if (vm.imageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.imageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.imageFile.CopyToAsync(stream);
                }
                product.ImageURL = fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
