using System.ComponentModel.DataAnnotations;

namespace MyShop.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        
        [Required] 
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required] 
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}