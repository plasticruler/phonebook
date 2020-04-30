using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure
{
    public interface IPhoneBookFacade:IPhoneBookRepository
    {
        public string GetLoginToken(string emailAddress, string password);
    }
}
