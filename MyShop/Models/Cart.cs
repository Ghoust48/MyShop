using System.Collections.Generic;
using System.Linq;

namespace MyShop.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        
        public User User { get; set; }
        
        //public string UserName { get; set; }

        public int Quantity => CartItems.Sum(item => item.Quantity);

        public decimal GrandTotal => CartItems.Sum(item => item.TotalPrice!.Value);

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}