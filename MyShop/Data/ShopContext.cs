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

            /*builder.Entity<User>().HasData(new User
            {
                FirstName = "Admin",
                LastName = "Admin",
                
            })*/

            builder.Entity<ProductWishlist>()
                .HasKey(table => new {table.ProductId, table.WishlistId});

            builder.Entity<ProductWishlist>()
                .HasOne(pw => pw.Product)
                .WithMany(p => p.ProductWishlists)
                .HasForeignKey(pw => pw.ProductId);

            builder.Entity<ProductWishlist>()
                .HasOne(pw => pw.Wishlist)
                .WithMany(w => w.ProductWishlists)
                .HasForeignKey(pw => pw.WishlistId);
            
            builder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);
            
            builder.Entity<User>()
                .HasOne(u => u.Wishlist)
                .WithOne(w => w.User)
                .HasForeignKey<Wishlist>(w => w.UserId);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Battery> Batteries { get; set; }

        public DbSet<Housing> Housings { get; set; }

        public DbSet<Memory> Memories { get; set; }

        public DbSet<Processor> Processors { get; set; }

        public DbSet<Screen> Screens { get; set; }
    }
}