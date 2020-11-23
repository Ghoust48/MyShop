using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    
    {
        private readonly ShopContext _context;

        public CategoriesController(ShopContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories
                .ToListAsync());
        }

        [HttpGet]
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
        public async Task<IActionResult> Edit(Category model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            var category = await _context.Categories.FindAsync(model.Id);

            if (category != null)
            {
                category.Name = model.Name;
                category.Description = model.Description;
                category.ImageName = model.ImageName;
                    
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(model);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
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