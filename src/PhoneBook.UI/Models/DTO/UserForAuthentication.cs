using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models.DTO
{
    public class UserForAuthentication
    {
     
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }        
        [Required]
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public bool RememberMe { get; set; }
    }
}
