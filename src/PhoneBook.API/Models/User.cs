using System.Collections.Generic;

namespace PhoneBook.API.Models
{
    public class User : BaseEntity<long>
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            PhoneBooks = new HashSet<UserPhoneBook>();
        }
        public string FirstName{get;set;}
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; } 
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserPhoneBook> PhoneBooks { get; set; }
             
    }
}