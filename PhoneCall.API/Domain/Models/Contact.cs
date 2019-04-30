using System.Collections;
using System.Collections.Generic;

namespace PhoneCall.API.Domain.Models{
    public class Contact:BaseEntity<int>{
        public string FirstName{get;set;}
        public string Surname{get;set;}
        public int UserId{get;set;}
        public User User{get;set;}
        public virtual IList<PhoneNumber> PhoneNumbers{get;set;} = new List<PhoneNumber>();

    }
}