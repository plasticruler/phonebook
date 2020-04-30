using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models.DTO
{
    public class AuthenticatedUser
    {
        public string JsonToken { get; set; }
        public string FirstName { get; set; }
        public long Id { get; set; }
    }
}
