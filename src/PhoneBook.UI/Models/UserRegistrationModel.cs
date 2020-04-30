using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models
{
    public class UserRegistrationModel

    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        
        [Required(ErrorMessage="Email is required")]
        [EmailAddress]
        public string EmailAddress { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="The password and confirmation password should match.")]
        public string ConfirmPassword { get; set; }
    }
}
