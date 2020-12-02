using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ShopContext _context;

        public WishlistController(ShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            var wishlists = await _context.Wishlists
                .Include(w => w.User)
                .Include(w => w.ProductWishlists)
                .ThenInclude(pw => pw.Product)
                .ToListAsync();
            
            return View(wishlists.FirstOrDefault(w => w.User.UserName == User.Identity.Name));
        }

        private async Task<Wishlist> Create()
        {
            var wishlist = new Wishlist
            {
                //UserName = User.Identity.Name,
            };

            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();

            return wishlist;
        }

        public async Task<IActionResult> Add(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            var list = await _context.Wishlists
                .Include(w => w.User)
                .Include(w => w.ProductWishlists)
                .ThenInclude(pw => pw.Product)
                .ToListAsync();

            var wishlist = list.FirstOrDefault(w => w.User.UserName == User.Identity.Name);
                           //?? await Create();

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);

            wishlist.ProductWishlists.Add(new ProductWishlist
            {
                //WishlistId = wishlist.Id,
                Wishlist = wishlist, 
                //ProductId = productId,
                Product = product
            });
            
            /*
            wishlist.ProductWishlists
                .Except(product.ProductWishlists)
                .ToList()
                .ForEach(productWishlist => wishlist.ProductWishlists.Add(productWishlist));
                */
            
            _context.Wishlists.Update(wishlist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int wishlistId, int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            var list = await _context.Wishlists
                .Include(w => w.User)
                .Include(w => w.ProductWishlists)
                .ThenInclude(pw => pw.Product)
                .ToListAsync();
            
            var model = list.FirstOrDefault(w => w.Id == wishlistId);

            if (model == null)
            {
                return NotFound();
            }
            
            var item = model.ProductWishlists.FirstOrDefault(p => p.ProductId == productId);
            model.ProductWishlists.Remove(item);

            _context.Wishlists.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}