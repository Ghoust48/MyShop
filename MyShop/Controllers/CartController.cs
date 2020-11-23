using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopContext _context;

        public CartController(ShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            var carts = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();
            
            return View(carts.FirstOrDefault(c=> c.UserName == User.Identity.Name));
        }

        private async Task<Cart> Create()
        {
            var cart = new Cart
            {
                UserName = User.Identity.Name,
                CartItems = new List<CartItem>()
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return cart;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var list = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();

            var cart = list.FirstOrDefault(c => c.UserName == User.Identity.Name)
                       ?? await Create();
            
            /*var cart = _context.Carts.FirstOrDefault(c => c.UserName == User.Identity.Name) 
                       ?? await Create();*/

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Product.Id == productId);

            if (cartItem == null)
            {
                cart.CartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity,
                    TotalPrice = quantity * product?.UnitPrice
                });
            }
            else
            {
                cartItem.Quantity = quantity == 0 ? ++quantity : quantity;
                cartItem.TotalPrice = cartItem.Quantity * cartItem.Product.UnitPrice;
            }

            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            var list = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();
            
            var cart = list.FirstOrDefault(c => c.UserName == User.Identity.Name);

            if (cart == null)
            {
                return NotFound();
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Product.Id == productId);

            if (cartItem == null)
            {
                return NotFound();
            }
            
            cart.CartItems.Remove(cartItem);

            _context.CartItems.Remove(cartItem);
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}