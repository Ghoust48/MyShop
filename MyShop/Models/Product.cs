using System.Collections.Generic;

namespace MyShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Slug { get; set; }
        
        public string Summary { get; set; }
        
        public string Description { get; set; }
        
        public string ImageFile { get; set; }
        
        public decimal? UnitPrice { get; set; }
        
        public int? UnitsInStock { get; set; }

        public Category Category { get; set; }
        
        public List<ProductWishlist> ProductWishlists { get; set; } = new List<ProductWishlist>();
    }
}