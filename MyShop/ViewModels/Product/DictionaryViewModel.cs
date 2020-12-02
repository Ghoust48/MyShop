using System.Collections.Generic;
using MyShop.Models;

namespace MyShop.ViewModels.Product
{
    public class DictionaryViewModel
    {
        public Dictionary<Category, List<Models.Product>> CategoryProducts { get; set; }

        public List<Models.Product> Products { get; set; }
    }
}