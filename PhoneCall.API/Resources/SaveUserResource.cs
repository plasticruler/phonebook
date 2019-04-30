using System.ComponentModel.DataAnnotations;

namespace PhoneCall.API.Resources{
    public class SaveUserResource{
        [Required]
        [MaxLength(50)]
        public string EmailAddress{get;set;}
    }
}