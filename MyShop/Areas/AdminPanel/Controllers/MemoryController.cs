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
    public class MemoryController : Controller
    {
        private readonly ShopContext _context;

        public MemoryController(ShopContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _context.Memories.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var memory = await _context.Memories.FindAsync(id);

            if (memory == null)
            {
                return NotFound();
            }

            return View(memory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Memory model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            var memory = await _context.Memories.FindAsync(model.Id);

            if (memory != null)
            {
                memory.PersistentMemory = model.PersistentMemory;
                memory.MemoryCardSupport = model.MemoryCardSupport;
                memory.RAM = model.RAM;
                    
                _context.Memories.Update(memory);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create(Memory model)
        {
            if (ModelState.IsValid)
            {
                _context.Memories.Add(model);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var memory = await _context.Memories.FindAsync(id);
            
            if (memory == null)
            {
                return NotFound();
            }

            _context.Memories.Remove(memory);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}