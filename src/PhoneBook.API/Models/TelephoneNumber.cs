namespace PhoneBook.API.Models
{    public class TelephoneNumber : BaseEntity<long>
    {
        public PhoneNumberType NumberType { get; set; }
        public string Number{get;set;}
    }
}
