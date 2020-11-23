namespace MyShop.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public Product Product { get; set; }
    }
}