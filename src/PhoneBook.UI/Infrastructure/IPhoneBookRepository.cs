using PhoneBook.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure
{
    public interface IPhoneBookRepository
    {
        //returns an authentication token
       string LoginUser(string emailAddress, string passwordHash);
       string CreateUser(string firstName, string surname, string password, string emailAddress);
       int CreateContact(int phoneBookId, string firstName, string surname, string phoneNumber, PhoneNumberType phoneNumberType);
        int CreatePhoneNumber(int contactId, string phoneNumber, PhoneNumberType numberType);
        bool DoesUserExist(string emailAddress);
       public IEnumerable<UserModel> GetUsers();
       public UserModel GetUser(int id);
       public IEnumerable<UserPhonebook> GetUserPhoneBooks(int userId);
        public UserPhonebook GetPhoneBook(int id);
        public ContactModel GetContact(int id);
        public TelephoneNumber GetTelephoneNumber(int id);
    }
}
