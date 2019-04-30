using System.Collections;
using System.Collections.Generic;

namespace PhoneCall.API.Domain.Models{
    public class User:BaseEntity<int>{
        public string EmailAddress{get;set;}
        public string PasswordHash{get;set;}
        public IList<Contact> Contacts{get;set;} = new List<Contact>();
    }
}