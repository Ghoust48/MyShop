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
    public class ProcessorController : Controller
    {
        private readonly ShopContext _context;

        public ProcessorController(ShopContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _context.Processors.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var processor = await _context.Processors.FindAsync(id);

            if (processor == null)
            {
                return NotFound();
            }

            return View(processor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Processor model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            var processor = await _context.Processors.FindAsync(model.Id);

            if (processor != null)
            {
                processor.Cores = model.Cores;
                processor.Microarchitecture = model.Microarchitecture;
                processor.Platform = model.Platform;
                processor.ClockSpeed = model.ClockSpeed;
                processor.GraphicsAccelerator = model.GraphicsAccelerator;
                processor.ProcessorType = model.ProcessorType;
                processor.TechnicalProcess = model.TechnicalProcess;
                processor.GPUFrequency = model.GPUFrequency;
                    
                _context.Processors.Update(processor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create(Processor model)
        {
            if (ModelState.IsValid)
            {
                _context.Processors.Add(model);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var processor = await _context.Processors.FindAsync(id);
            
            if (processor == null)
            {
                return NotFound();
            }

            _context.Processors.Remove(processor);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}