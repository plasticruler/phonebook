using PhoneBook.UI.Infrastructure.Messager;
using PhoneBook.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure
{
    public class PhoneBookOrchestrator : IPhoneBookOrchestrator<long>
    {
        private readonly IMessager _messager;

        public PhoneBookOrchestrator(IMessager messager)
        {
            _messager = messager;
        }
        public long CreateUser(string firstName, string surname, string password, string emailAddress)
        {
            UserModel um = new UserModel()
            {
                EmailAddress = emailAddress,
                FirstName = firstName,
                Surname = surname,

            };
            return 3;
        }
    }
}
