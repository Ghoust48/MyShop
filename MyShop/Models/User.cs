using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyShop.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Cart Cart { get; set; }

        public Wishlist Wishlist { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}