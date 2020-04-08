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
        public string PasswordHash { get; set; }
        public IEnumerable<UserPhonebook> PhoneBooks { get; set; }
    }
}
