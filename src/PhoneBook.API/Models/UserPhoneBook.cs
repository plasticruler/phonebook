using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PhoneBook.API.Models
{
    public class UserPhoneBook : BaseEntity<long>
    {
        public UserPhoneBook()
        {

        }
        public long UserId { get; set; }
        [JsonIgnore]
        public User User{get;set;}
        public string Name{get;set;}  
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
