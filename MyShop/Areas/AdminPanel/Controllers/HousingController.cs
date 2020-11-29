using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [Authorize(Roles="admin")]
    public class HousingController : Controller
    {
        private readonly ShopContext _context;

        public HousingController(ShopContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _context.Housings.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var housing = await _context.Housings.FindAsync(id);

            if (housing == null)
            {
                return NotFound();
            }

            return View(housing);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Housing model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            var housing = await _context.Housings.FindAsync(model.Id);

            if (housing != null)
            {
                housing.Design = model.Design;
                housing.Colour = model.Colour;
                housing.BodyMaterial = model.BodyMaterial;
                housing.GlassConstruction = model.GlassConstruction;
                    
                _context.Housings.Update(housing);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create(Housing model)
        {
            if (ModelState.IsValid)
            {
                _context.Housings.Add(model);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var housing = await _context.Housings.FindAsync(id);
            
            if (housing == null)
            {
                return NotFound();
            }

            _context.Housings.Remove(housing);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}