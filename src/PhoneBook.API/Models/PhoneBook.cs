using System.Collections.Generic;

namespace PhoneBook.API.Models
{
    public class UserPhoneBook : BaseEntity<long>
    {
        public UserPhoneBook()
        {

        }
        public long UserId { get; set; }
        public User User{get;set;}
        public string Name{get;set;}  
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
