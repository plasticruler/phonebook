using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.API.Models
{
    public class Contact : BaseEntity<long>
    {
        public UserPhoneBook PhoneBook{get;set;}
        [Required]
        [MinLength(5, ErrorMessage = "First Name invalid")]
        public string FirstName {get;set;} 
        [Required]
        public string Lastname { get; set; }
        public IEnumerable<TelephoneNumber> PhoneNumbers { get; set; }
    }
}