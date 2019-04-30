using System.ComponentModel.DataAnnotations;

namespace PhoneCall.API.Resources{
    public class SavePhoneNumberResource{//from front-end
        public int ID{get;set;}
        [Required]
        [MaxLength(20)]
        public string Number{get;set;}
        [Required]
        [Range(1,5)]
        public int PhoneNumberType{get;set;}
        [Required]
        public int ContactID{get;set;}
        [Required]
        public int UserID{get;set;}

    }
}