using PhoneBook.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure
{
    public class PhoneBookRepository : IPhoneBookRepository
    {
        public int CreateContact(int phoneBookId, string firstName, string surname, string phoneNumber, PhoneNumberType phoneNumberType)
        {
            throw new NotImplementedException();
        }

        public int CreatePhoneNumber(int contactId, string phoneNumber, PhoneNumberType numberType)
        {
            throw new NotImplementedException();
        }

        public string CreateUser(string firstName, string surname, string password, string emailAddress)
        {
            throw new NotImplementedException();
        }

        public bool DoesUserExist(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public ContactModel GetContact(int id)
        {
            throw new NotImplementedException();
        }

        public UserPhonebook GetPhoneBook(int id)
        {
            throw new NotImplementedException();
        }

        public TelephoneNumber GetTelephoneNumber(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserPhonebook> GetUserPhoneBooks(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetUsers()
        {
            throw new NotImplementedException();
        }

        public string LoginUser(string emailAddress, string passwordHash)
        {
            throw new NotImplementedException();
        }
    }
}
