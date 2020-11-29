using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Display(Name = "Слуг")]
        public string Slug { get; set; }
        
        [Display(Name = "Краткое описание")]
        public string Summary { get; set; }
        
        [Display(Name = "Полное описание")]
        public string Description { get; set; }
        
        [Display(Name = "Картинка")]
        public string ImageFile { get; set; }
        
        [Display(Name = "Цена")]
        public decimal? UnitPrice { get; set; }
        
        [Display(Name = "Количество")]
        public int? UnitsInStock { get; set; }

        [Display(Name = "Категория")]
        public Category Category { get; set; }

        [Display(Name = "Битарея")]
        public Battery Battery { get; set; }

        [Display(Name = "Корпус")]
        public Housing Housing { get; set; }

        [Display(Name = "Память")]
        public Memory Memory { get; set; }

        [Display(Name = "Процессор")]
        public Processor Processor { get; set; }

        [Display(Name = "Дисплей")]
        public Screen Screen { get; set; }
        public List<ProductWishlist> ProductWishlists { get; set; } = new List<ProductWishlist>();
    }
}