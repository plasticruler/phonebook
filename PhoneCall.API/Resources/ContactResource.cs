using System.ComponentModel.DataAnnotations;

namespace PhoneCall.API.Resources{
    public class ContactResource{
        public int ID{get;set;}
        [Required]
        [MaxLength(50)]
        public string FirstName{get;set;}
        [Required]
        [MaxLength(50)]
        public string Surname{get;set;}
        [Required]
        public int UserID{get;set;}
    }
}