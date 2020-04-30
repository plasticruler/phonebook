using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.API.Models.DTO
{
    public class ContactForCreate
    {
        public long PhoneBookId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PhoneNumberType PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}
