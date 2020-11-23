using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;

namespace MyShop.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly ShopContext _context;

        public OrderController(ShopContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.ShippingAddress)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return View(orders);
        }
    }
}