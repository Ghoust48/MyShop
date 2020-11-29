using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyShop.Models;

namespace MyShop.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Слуг")]
        public string Slug => Name.Trim(' ');
        
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

        public int CategoryId { get; set; }

        public int HousingId { get; set; }

        public int MemoryId { get; set; }

        public int ProcessorId { get; set; }

        public int ScreenId { get; set; }

        public int BatteryId { get; set; }
        
        [Display(Name = "Экран")]
        public IEnumerable<Screen> Screens { get; set; }
        
        [Display(Name = "Процессор")]
        public IEnumerable<Processor> Processors { get; set; }
        
        [Display(Name = "Память")]
        public IEnumerable<Memory> Memories { get; set; }

        [Display(Name = "Корпус")]
        public IEnumerable<Housing> Housings { get; set; }
        
        [Display(Name = "Аккамулятор")]
        public IEnumerable<Battery> Batteries { get; set; }
        
        [Display(Name = "Категория")]
        public IEnumerable<Category> Categories { get; set; }
    }
}