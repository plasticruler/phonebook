using PhoneBook.UI.Models;
using PhoneBook.UI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure
{
    public interface IPhoneBookRepository
    {
        //returns an authentication token /place elsewhere though
       AuthenticatedUser LoginUser(string emailAddress, string password);
       string CreateUser(string firstName, string surname, string password, string emailAddress);
       int CreateContact(int phoneBookId, string firstName, string surname, string phoneNumber, PhoneNumberType phoneNumberType);
        int CreatePhoneNumber(int contactId, string phoneNumber, PhoneNumberType numberType);
        UserPhonebook CreatePhoneBook(int userId, string phoneBookName);

        bool DoesUserExist(string emailAddress);
       public IEnumerable<UserModel> GetUsers();
       public UserModel GetUser(int id);
       public IEnumerable<UserPhonebook> GetUserPhoneBooks(int userId);
        public UserPhonebook GetPhoneBook(int id);
        public Contact GetContact(int id);
        public TelephoneNumber GetTelephoneNumber(int id);
        void SetAuthKey(string jwtToken);
    }
}
