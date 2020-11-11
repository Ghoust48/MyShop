using System.ComponentModel.DataAnnotations;

namespace MyShop.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string NewPassword { get; set; }
    }
}