using AutoMapper;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Resources;
using PhoneCall.API.Extensions;
using PhoneCall.API.Domain.Enums;

namespace PhoneCall.API.Mapping{
    public class MappingProfiles: Profile{
        public MappingProfiles(){
            CreateMap<User, UserResource>();
            CreateMap<Contact, ContactResource>();
            CreateMap<PhoneNumber, PhoneNumberResource>()
                .ForMember(src => src.PhoneNumberType,
                             opt => opt.MapFrom(src => src.PhoneNumberType.ToDescriptionString()));
            ///////////
            CreateMap<SaveContactResource, Contact>();
            CreateMap<SaveUserResource, User>();
            CreateMap<SavePhoneNumberResource, PhoneNumber>()
                .ForMember(src=>src.PhoneNumberType, opt=>opt.MapFrom(src=>(EPhoneNumberType)src.PhoneNumberType));
                
        }
        
    }
}