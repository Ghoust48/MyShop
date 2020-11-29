using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using MyShop.ViewModels.Product;

namespace MyShop.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [Authorize(Roles="admin")]
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
                    .Include(model => model.Category)
                    .AsNoTracking()
                    .ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Battery)
                .Include(p => p.Housing)
                .Include(p => p.Memory)
                .Include(p => p.Processor)
                .Include(p => p.Screen)
                .ToListAsync();

            var product = products.FirstOrDefault(item => item.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            
            var model = new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Summary = product.Summary,
                ImageFile = product.ImageFile,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                CategoryId = product.Category.Id,
                BatteryId = product.Battery.Id,
                HousingId = product.Housing.Id,
                MemoryId = product.Memory.Id,
                ScreenId = product.Screen.Id,
                ProcessorId = product.Processor.Id,
                Categories = _context.Categories.Distinct().ToList(),
                Batteries = _context.Batteries.Distinct().ToList(),
                Housings = _context.Housings.Distinct().ToList(),
                Memories = _context.Memories.Distinct().ToList(),
                Screens = _context.Screens.Distinct().ToList(),
                Processors = _context.Processors.Distinct().ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            var product = await _context.Products.FindAsync(model.Id);

            if (product != null)
            {
                product.Name = model.Name;
                product.Slug = model.Slug;
                product.Summary = model.Summary;
                product.Description = model.Description;
                product.ImageFile = model.ImageFile;
                product.UnitPrice = model.UnitPrice;
                product.UnitsInStock = model.UnitsInStock;

                product.Battery = _context.Batteries.FirstOrDefault(battery => battery.Id == model.BatteryId);
                product.Housing = _context.Housings.FirstOrDefault(housing => housing.Id == model.HousingId);
                product.Memory = _context.Memories.FirstOrDefault(memory => memory.Id == model.MemoryId);
                product.Processor =
                    _context.Processors.FirstOrDefault(processor => processor.Id == model.ProcessorId);
                product.Screen = _context.Screens.FirstOrDefault(screen => screen.Id == model.ScreenId);
                product.Category = _context.Categories.FirstOrDefault(category => category.Id == model.CategoryId);
                    
                model.Categories = _context.Categories.Distinct().ToList();
                model.Batteries = _context.Batteries.Distinct().ToList();
                model.Housings = _context.Housings.Distinct().ToList();
                model.Memories = _context.Memories.Distinct().ToList();
                model.Screens = _context.Screens.Distinct().ToList();
                model.Processors = _context.Processors.Distinct().ToList();
                    
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var product = new ProductViewModel()
            {
                Categories = _context.Categories.Distinct().ToList(),
                Batteries = _context.Batteries.Distinct().ToList(),
                Housings = _context.Housings.Distinct().ToList(),
                Memories = _context.Memories.Distinct().ToList(),
                Screens = _context.Screens.Distinct().ToList(),
                Processors = _context.Processors.Distinct().ToList()
            };
            
            return View(product);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Slug = model.Slug,
                    Summary = model.Summary,
                    ImageFile = model.ImageFile,
                    UnitPrice = model.UnitPrice,
                    UnitsInStock = model.UnitsInStock,
                    Category = _context.Categories.FirstOrDefault(category => category.Id == model.CategoryId),
                    Battery = _context.Batteries.FirstOrDefault(battery => battery.Id == model.BatteryId),
                    Housing = _context.Housings.FirstOrDefault(housing => housing.Id == model.HousingId),
                    Memory = _context.Memories.FirstOrDefault(memory => memory.Id == model.MemoryId),
                    Processor = _context.Processors.FirstOrDefault(processor => processor.Id == model.ProcessorId),
                    Screen = _context.Screens.FirstOrDefault(screen => screen.Id == model.ScreenId)
                };
                
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}