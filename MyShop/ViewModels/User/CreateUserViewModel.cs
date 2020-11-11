using System.ComponentModel.DataAnnotations;

namespace MyShop.ViewModels
{
    public class CreateUserViewModel
    {
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
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}