using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Models;

namespace MyShop.ViewModels.Product
{
    public class ProductCategoryViewModel
    {
        public List<Models.Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public string Category { get; set; }
        public string Search { get; set; }
    }
}