using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models
{
    public class TelephoneNumber:BaseModel
    {
        public int ContactId { get; set; }
        public PhoneNumberType NumberType { get; set; }
        [Required]
        public string Number { get; set; }
    }
}
