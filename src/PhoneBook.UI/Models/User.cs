using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models
{
    public class User
    {
        public string JwtToken { get; set; }
        public string EmailAddress { get; set; }
        public string Salutation { get; set; }
    }
}
