using System.Collections.Generic;

namespace MyShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string ImageName { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}