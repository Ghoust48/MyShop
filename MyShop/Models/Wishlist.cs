using System.Collections.Generic;

namespace MyShop.Models
{
    public class Wishlist
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        
        public User User { get; set; }
        
        //public string UserName { get; set; }

        public List<ProductWishlist> ProductWishlists { get; set; } = new List<ProductWishlist>();
    }
}