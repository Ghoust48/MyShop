namespace MyShop.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal? TotalPrice { get; set; }
        
        public Product Product { get; set; }
    }
}