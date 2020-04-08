namespace PhoneBook.API.Models
{
    public class User : BaseEntity<long>
    {
        public string FirstName{get;set;}
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; } 
             
    }
}