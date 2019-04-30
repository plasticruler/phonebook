using System.Collections;
using PhoneCall.API.Domain.Enums;

namespace PhoneCall.API.Domain.Models{
    public class PhoneNumber:BaseEntity<int>
    {
        public string Number{get;set;}
        public  int ContactId{get;set;}
        public Contact Contact{get;set;}
        public EPhoneNumberType PhoneNumberType{get;set;}
        public int UserId{get;set;}
        public User User{get;set;}
    }
}