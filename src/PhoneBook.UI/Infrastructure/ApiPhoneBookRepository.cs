using Microsoft.Extensions.Configuration;
using PhoneBook.UI.Infrastructure.Messager;
using PhoneBook.UI.Models;
using PhoneBook.UI.Models.DTO;
using PhoneBook.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure
{
    public class ApiPhoneBookRepository : IPhoneBookRepository
    {
        private readonly IMessager _messager;
        private string _jwtToken;

        private readonly IConfiguration _configuration;
        private string ApiUrl => GetConfigValue("ApiUrl");

        public ApiPhoneBookRepository(IMessager messager, IConfiguration configuration)
        {
            _messager = messager;
            _configuration = configuration;
        }
        public int CreateContact(int phoneBookId, string firstName, string surname, string phoneNumber, PhoneNumberType phoneNumberType)
        {
            _messager.Post<ContactForCreate, string>(GetRemoteUrl("Contact/CreateForContact"), new ContactForCreate
            {
                FirstName = firstName,
                LastName = surname,
                PhoneBookId = phoneBookId,
                PhoneNumberType = phoneNumberType,
                PhoneNumber = phoneNumber
            }
            , _jwtToken);
            return 0;
        }

        public int CreatePhoneNumber(int contactId, string phoneNumber, PhoneNumberType numberType)
        {
            _messager.Post<TelephoneNumberForCreate, string>(GetRemoteUrl("Telephone/CreateForTelephoneNumber"), new TelephoneNumberForCreate()
            {
                ContactId = contactId,
                Number = phoneNumber,
                NumberType = numberType
            }, _jwtToken);

            return 0;
        }

        public string CreateUser(string firstName, string surname, string password, string emailAddress)
        {
            throw new NotImplementedException();
        }

        public bool DoesUserExist(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(int id)
        {
            return _messager.Get<Contact>(GetRemoteUrl("Contact", id.ToString()), _jwtToken).Result;
        }

        public UserPhonebook GetPhoneBook(int id)
        {
            return _messager.Get<UserPhonebook>(GetRemoteUrl("PhoneBook", id.ToString()), _jwtToken).Result;
        }

        public TelephoneNumber GetTelephoneNumber(int id)
        {
            return _messager.Get<TelephoneNumber>(GetRemoteUrl("Telephone", id.ToString()), _jwtToken).Result;
        }

        public UserModel GetUser(int id)
        {
            return _messager.Get<UserModel>(GetRemoteUrl("Users", id.ToString()), _jwtToken).Result;
        }

        public IEnumerable<UserPhonebook> GetUserPhoneBooks(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return _messager.Get<IEnumerable<UserModel>>(GetRemoteUrl("Users"), _jwtToken).Result;
        }

        protected string GetConfigValue(string key, string defaultValue = "", bool raiseException = false)
        {
            string value = _configuration[$"AppSettings:{key}"];
            if (string.IsNullOrWhiteSpace(value) && raiseException)
                throw new ApplicationException($"Key '{key}' not found.");
            return value ?? defaultValue;
        }

        protected string GetRemoteUrl(string url, params string[] parameters)
        {
            var p = string.Join("/", parameters);
            if (parameters.Any())
            {
                return $"{ApiUrl}/{url}/{p}";
            }
            return $"{ApiUrl}/{url}";
        }
        public AuthenticatedUser LoginUser(string emailAddress, string passwordHash)
        {
            var payload = new UserForAuthentication
            {
                EmailAddress = emailAddress,
                PasswordHash = passwordHash
            };

            return _messager.Post<UserForAuthentication, AuthenticatedUser>(GetRemoteUrl("Login"), payload).Result;
        }

        public void SetAuthKey(string jwtToken)
        {
            _jwtToken = jwtToken;
        }

        public UserPhonebook CreatePhoneBook(int userId, string phoneBookName)
        {
            var result = _messager.Post<PhoneBookForCreate, UserPhonebook>(GetRemoteUrl("PhoneBook"),
                new PhoneBookForCreate
                {
                    Name = phoneBookName,
                    UserId = userId
                },
                _jwtToken).Result;
            return result;
        }
    }
}
