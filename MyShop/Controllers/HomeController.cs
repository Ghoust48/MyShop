using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyShop.Data;
using MyShop.Models;
using MyShop.ViewModels.Product;

namespace MyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ShopContext _context;

        public HomeController(ILogger<HomeController> logger, ShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            /*return View(await _context.Products
                .Include(p => p.Category)
                .ToListAsync());*/
            var products = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            var categoryQuery = _context.Categories.OrderBy(c => c.Name);

            var productCategory = new ProductCategoryViewModel
            {
                Categories = new List<Models.Category>(await categoryQuery.Distinct().ToListAsync()),
                Products = await products.ToListAsync()
            };

            return View(productCategory);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}