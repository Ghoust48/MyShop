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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ProductWishlist>()
                .HasKey(table => new { table.ProductId, table.WishlistId });

            builder.Entity<ProductWishlist>()
                .HasOne(pw => pw.Product)
                .WithMany(p => p.ProductWishlists)
                .HasForeignKey(pw => pw.ProductId);

            builder.Entity<ProductWishlist>()
                .HasOne(pw => pw.Wishlist)
                .WithMany(w => w.ProductWishlists)
                .HasForeignKey(pw => pw.WishlistId);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}