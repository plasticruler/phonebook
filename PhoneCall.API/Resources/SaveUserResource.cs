using System.ComponentModel.DataAnnotations;

namespace PhoneCall.API.Resources{
    public class SaveUserResource{
        [Required]
        [MaxLength(50)]
        public string EmailAddress{get;set;}
        
        [Required]
        [MaxLength(128)]
        public string PasswordHash{ get; set; }
    }
}