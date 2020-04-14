using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure
{
    public interface IPhoneBookOrchestrator<T>
    {
        T CreateUser(string firstName, string surname, string password, string emailAddress);
    }
}
