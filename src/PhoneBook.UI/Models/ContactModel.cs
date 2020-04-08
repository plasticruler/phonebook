using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models
{
    public class ContactModel:BaseModel
    {
        
        public ContactModel()
        {
            PhoneNumbers = new List<TelephoneNumber>();
        }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }       
        public UserPhonebook PhoneBook { get; set; }
        public int PhoneBookId { get; set; }
        public List<TelephoneNumber> PhoneNumbers { set; get; }        
        public string PhoneNumber { get; set; }
        public string PhoneNumberType { get; set; }

    }
}
