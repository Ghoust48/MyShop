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
        public async Task<IActionResult> Index(string category, string search, int? pageNumber, int pageSize = 8)
        {
            var products = _context.Products
                .Include(p => p.Category)
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
            
            return View(await PaginatedList<Product>.CreateAsync(products, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult Details(string slug)
        {
            return View(_context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Slug.ToLower().Contains(slug.ToLower())));
        }
        
    }
}