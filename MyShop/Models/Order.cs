using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        

        /*public User User { get; set; }*/

        public decimal GrandTotal { get; set; }
        
        [Required]
        public Address ShippingAddress { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }
        
        public OrderStatus Status { get; set; }
    }
    
    public enum OrderStatus
    {
        Progress = 1,
        OnShipping = 2,
        Finished = 3
    }

    public enum PaymentMethod
    {
        Check = 1,
        BankTransfer = 2,
        Cash = 3,
        Paypal = 4,
        Payoneer = 5
    }
}