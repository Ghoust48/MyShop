using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;

namespace MyShop.Views.Shared.Components.CartCounter
{
    public class CartCounterViewComponent : ViewComponent
    {
        private readonly ShopContext _context;

        public CartCounterViewComponent(ShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var carts = await _context.Carts
                    .Include(c => c.CartItems)
                    .Include(c => c.User)
                    .ToListAsync();

                var cart = carts.FirstOrDefault(c => c.User.UserName == User.Identity.Name);
                return View(cart.Quantity);
            }

            return View(0);
        }
    }
}