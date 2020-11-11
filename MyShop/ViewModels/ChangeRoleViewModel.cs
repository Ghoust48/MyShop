using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyShop.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        
        public string UserEmail { get; set; }
        
        public List<IdentityRole> AllRoles { get; set; } = new List<IdentityRole>();
        
        public IList<string> UserRoles { get; set; } = new List<string>();
        
    }
}