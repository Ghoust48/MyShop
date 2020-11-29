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
    public class BatteryController : Controller
    {
        private readonly ShopContext _context;

        public BatteryController(ShopContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _context.Batteries.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var battery = await _context.Batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return View(battery);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Battery model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            var battery = await _context.Batteries.FindAsync(model.Id);

            if (battery != null)
            {
                battery.Type = model.Type;
                battery.Capacity = model.Capacity;
                battery.MaxWorkingHours = model.MaxWorkingHours;
                    
                _context.Batteries.Update(battery);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create(Battery model)
        {
            if (ModelState.IsValid)
            {
                _context.Batteries.Add(model);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var battery = await _context.Batteries.FindAsync(id);
            
            if (battery == null)
            {
                return NotFound();
            }

            _context.Batteries.Remove(battery);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}