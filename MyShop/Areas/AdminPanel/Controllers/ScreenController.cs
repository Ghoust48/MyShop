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
    public class ScreenController : Controller
    {
        private readonly ShopContext _context;

        public ScreenController(ShopContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _context.Screens.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var screen = await _context.Screens.FindAsync(id);

            if (screen == null)
            {
                return NotFound();
            }

            return View(screen);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Screen model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            var screen = await _context.Screens.FindAsync(model.Id);

            if (screen != null)
            {
                screen.Diagonal = model.Diagonal;
                screen.Multitouch = model.Multitouch;
                screen.Resolution = model.Resolution;
                screen.Surface = model.Surface;
                screen.MatrixType = model.MatrixType;
                    
                _context.Screens.Update(screen);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create(Screen model)
        {
            if (ModelState.IsValid)
            {
                _context.Screens.Add(model);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var screen = await _context.Screens.FindAsync(id);
            
            if (screen == null)
            {
                return NotFound();
            }

            _context.Screens.Remove(screen);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}