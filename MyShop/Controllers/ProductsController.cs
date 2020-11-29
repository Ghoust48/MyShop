using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Index(string sortOrder, string category, string search, int? pageNumber, int pageSize = 8)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PriceSortParm"] = string.IsNullOrEmpty(sortOrder) ? "price-desc" : "";
            
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Battery)
                .Include(p => p.Housing)
                .Include(p => p.Memory)
                .Include(p => p.Processor)
                .Include(p => p.Screen)
                .AsQueryable();

            if (search != null)
            {
                pageNumber = 1;
            }

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.ToLower()
                    .Contains(search.ToLower()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Name.ToLower()
                    .Contains(category.ToLower()));
            }
            
            switch (sortOrder)
            {
                case "price-desc":
                    products = products.OrderByDescending(p => p.UnitPrice);
                    break;
                default:
                    products = products.OrderBy(p => p.UnitPrice);
                    break;
            }

            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(string sortOrder, string category, string search, int? pageNumber, int pageSize = 8)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PriceSortParm"] = string.IsNullOrEmpty(sortOrder) ? "price-desc" : "";
            
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Battery)
                .Include(p => p.Housing)
                .Include(p => p.Memory)
                .Include(p => p.Processor)
                .Include(p => p.Screen)
                .AsQueryable();

            if (search != null)
            {
                pageNumber = 1;
            }

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.ToLower()
                    .Contains(search.ToLower()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Name.ToLower()
                    .Contains(category.ToLower()));
            }
            
            switch (sortOrder)
            {
                case "price-desc":
                    products = products.OrderByDescending(p => p.UnitPrice);
                    break;
                default:
                    products = products.OrderBy(p => p.UnitPrice);
                    break;
            }
            
                return PartialView("_ProductsAjax",
                    await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        [HttpGet]
        public IActionResult Details(string slug)
        {
            return View(_context.Products
                .Include(p => p.Category)
                .Include(p => p.Battery)
                .Include(p => p.Housing)
                .Include(p => p.Memory)
                .Include(p => p.Processor)
                .Include(p => p.Screen)
                .FirstOrDefault(p => p.Slug.ToLower().Contains(slug.ToLower())));
        }
        
    }
}