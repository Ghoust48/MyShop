using Microsoft.AspNetCore.Identity;

namespace MyShop.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}