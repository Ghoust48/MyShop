using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyShop.Data;

namespace MyShop.Views.Shared.Components.WishlistCounter
{
    public class WishlistCounterViewComponent : ViewComponent
    {
        private readonly ShopContext _context;

        public WishlistCounterViewComponent(ShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var wishlists = await _context.Wishlists
                    .Include(w => w.User)
                    .Include(w => w.ProductWishlists)
                    .ToListAsync();

                var wishlist = wishlists.FirstOrDefault(w => w.User.UserName == User.Identity.Name);
            
                return View(wishlist.ProductWishlists.Count);
            }

            return View(0);
        }
    }
}