using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models
{
    public class UserModel : BaseModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$", ErrorMessage = "Password not complex enough.")]
        [Compare("Password", ErrorMessage = "Passwords should match.")]
        public string RetypedPassword { get; set; }
        public IEnumerable<UserPhonebook> PhoneBooks { get; set; }
    }
}
