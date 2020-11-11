using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Data
{
    public class ShopContext : IdentityDbContext<User>
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<ProductModel> Products { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }
    }
}