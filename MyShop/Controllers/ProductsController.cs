using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using MyShop.ViewModels.Product;

namespace MyShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
                .Include(model => model.Category)
                .ToListAsync());
        }

        [HttpGet]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _context.Products
                .Include(model => model.Category)
                .ToListAsync();

            var product = products.FirstOrDefault(item => item.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            
            var model = new EditProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Slug = product.Slug,
                Summary = product.Summary,
                ImageFile = product.ImageFile,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                CategoryId = product.Category.Id,
                Categories = _context.Categories.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
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

                    product.Category = _context.Categories.FirstOrDefault(category => category.Id == model.CategoryId);
                    model.Categories = _context.Categories.ToList();
                    
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                }
            }
            
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles="admin")]
        public IActionResult Create()
        {
            var product = new EditProductViewModel()
            {
                Categories = _context.Categories.ToList()
            };
            
            return View(product);
        }
        
        [HttpPost]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> Create(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new ProductModel
                {
                    Name = model.Name,
                    Description = model.Description,
                    Slug = model.Slug,
                    Summary = model.Summary,
                    ImageFile = model.ImageFile,
                    UnitPrice = model.UnitPrice,
                    UnitsInStock = model.UnitsInStock,
                    Category = _context.Categories.FirstOrDefault(category => category.Id == model.CategoryId)
                };
                
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Authorize(Roles="admin")]
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