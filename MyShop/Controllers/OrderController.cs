using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using Wkhtmltopdf.NetCore;

namespace MyShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShopContext _context;

        private readonly IGeneratePdf _generatePdf;

        public OrderController(ShopContext context, IGeneratePdf generatePdf)
        {
            _context = context;
            _generatePdf = generatePdf;
        }

        public async Task<IActionResult> Add(int cartId)
        {
            var carts = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();
            
            var cart = carts.FirstOrDefault(c => c.Id == cartId);

            if (cart == null)
            {
                return NotFound();
            }

            var order = new Order
            {
                UserName = cart.UserName,
                Status = OrderStatus.Progress,
                ShippingAddress = new Address(),
                GrandTotal = cart.GrandTotal,
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    Product = ci.Product,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice.Value
                }).ToList()
            };
            
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Order orderModel)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserName == User.Identity.Name);

            orderModel.UserName = cart.UserName;
            orderModel.GrandTotal = cart.GrandTotal;
            orderModel.OrderItems = cart.CartItems.Select(ci => new OrderItem
            {
                Product = ci.Product,
                Quantity = ci.Quantity,
                TotalPrice = ci.TotalPrice.Value
            }).ToList();
            
            if (ModelState.IsValid)
            {
                _context.Orders.Add(orderModel);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Finish), orderModel);
            }

            return View(orderModel);
        }

        /*[HttpGet]
        public async Task<IActionResult> Finish(Order order)
        {
            var carts = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();

            var cart = carts.FirstOrDefault(c => c.UserName == User.Identity.Name);
            cart?.CartItems.Clear();

            _context.Update(cart);
            await _context.SaveChangesAsync();
            return View(order);
        }
        */

        [HttpGet]
        public async Task<IActionResult> Finish(Order orderModel)
        {
            try
            {
                await ClearCart();
            }
            catch (NullReferenceException e)
            {
                return NotFound();
            }
            
            var orders = await _context.Orders
                .Include(o => o.ShippingAddress)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            var order = orders.FirstOrDefault(o => o.Id == orderModel.Id);

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Invoice(int orderId)
        {
            var orders = await _context.Orders
                .Include(o => o.ShippingAddress)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            var order = orders.FirstOrDefault(o => o.Id == orderId);
            
            return await _generatePdf.GetPdf("Views/Order/Invoice.cshtml", order);
        }

        private async Task ClearCart()
        {
            var carts = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();

            var cart = carts.FirstOrDefault(c => c.UserName == User.Identity.Name);

            if (cart == null)
            {
                throw new NullReferenceException(nameof(cart));
            }
            
            cart.CartItems.Clear();

            _context.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}