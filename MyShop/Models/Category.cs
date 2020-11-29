using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Display(Name = "Описание")]
        public string Description { get; set; }
        
        [Display(Name = "Картинка")]
        public string ImageName { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}