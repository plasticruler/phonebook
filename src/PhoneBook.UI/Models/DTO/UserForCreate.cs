using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models.DTO
{
    public class UserForCreate
    {
       
        public string FirstName { get; set; }       
      
        public string Surname { get; set; }        
        
        public string EmailAddress { get; set; }       
       
        public string Password { get; set; }   
    }
}
