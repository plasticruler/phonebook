using AutoMapper;
using PhoneBook.UI.Models;
using PhoneBook.UI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure.Mapper
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<UserModel, UserForCreate>();
        }
    }
}
