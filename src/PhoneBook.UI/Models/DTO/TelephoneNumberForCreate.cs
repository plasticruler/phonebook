using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models.DTO
{
    public class TelephoneNumberForCreate
    {
        public long ContactId { get; set; }
        public string Number { get; set; }
        public PhoneNumberType NumberType { get; set; }
    }
}
