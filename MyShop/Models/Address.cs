using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Address
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }
        
        public string PhoneNo { get; set; }
        
        public string AddressLine { get; set; }
        
        public string Country { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        
        public string ZipCode { get; set; }
        
        /*public int Id { get; set; }
        
        [Required]
        [MinLength(5), MaxLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(5), MaxLength(100)]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        
        [Required]
        [MinLength(10), MaxLength(100)]
        public string AddressLine { get; set; }
        
        [Required]
        [MinLength(5), MaxLength(100)]
        public string Country { get; set; }
        
        [Required]
        [MinLength(5), MaxLength(100)]
        public string City { get; set; }
        
        [Required]
        [MinLength(5), MaxLength(100)]
        public string State { get; set; }
        
        [Required]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }*/
    }
}