using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

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
                .Include(o => o.User)
                .Include(o => o.ShippingAddress)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int orderId)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.ShippingAddress)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
            
            /*var orders1 = _context.Orders
                .Include(o => o.ShippingAddress)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.Id == orderId)
                .AsEnumerable();*/

            return View(orders.FirstOrDefault(o => o.Id == orderId));
        }
    }
}