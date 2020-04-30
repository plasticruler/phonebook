using System.Text.Json.Serialization;

namespace PhoneBook.API.Models
{    public class TelephoneNumber : BaseEntity<long>
    {
        public PhoneNumberType NumberType { get; set; }
        public string Number{get;set;}
        public long ContactId { get; set; }
        [JsonIgnore]
        public Contact Contact { get; set; }
    }

}
