using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ShopContext _context;

        public CategoriesController(ShopContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories
                .ToListAsync());
        }

        [HttpGet]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> Edit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = await _context.Categories.FindAsync(model.Id);

                if (category != null)
                {
                    category.Name = model.Name;
                    category.Description = model.Description;
                    category.ImageName = model.ImageName;
                    
                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync();
                }
            }
            
            return View(model);
        }
        
        public IActionResult Create() => View();
        
        [HttpPost]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(model);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Authorize(Roles="admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}