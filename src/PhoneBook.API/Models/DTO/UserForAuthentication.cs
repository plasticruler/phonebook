using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.API.Models.DTO
{
    public class UserForAuthentication<T>
    {
        public T Id { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
    }
}
