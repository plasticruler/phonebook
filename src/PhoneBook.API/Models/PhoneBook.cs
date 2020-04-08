namespace PhoneBook.API.Models
{
    public class UserPhoneBook : BaseEntity<long>
    {
        public int OwnerId { get; set; }
        public User Owner{get;set;}
        public string Name{get;set;}        
    }
}
