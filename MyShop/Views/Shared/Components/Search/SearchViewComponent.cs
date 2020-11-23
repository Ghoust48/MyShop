using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using MyShop.ViewModels.Product;

namespace MyShop.Views.Shared.Components.Search
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly ShopContext _context;

        public SearchViewComponent(ShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
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
    }
}