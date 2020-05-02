using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Models
{
    public class UserPhonebook:BaseModel
    {        
        public UserPhonebook()
        {
            Contacts = new List<Contact>();
        }
        public long UserId { get; set; }
        public UserModel User { get; set; }
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
