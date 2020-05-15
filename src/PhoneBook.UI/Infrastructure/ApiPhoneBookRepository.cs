using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PhoneBook.UI.Configuration;
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

        private readonly IOptions<AppSettings> _configuration;        

        public ApiPhoneBookRepository(IMessager messager, IOptions<AppSettings> appSettings)
        {
            _messager = messager;
            _configuration = appSettings;            
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
            _messager.Post<UserForCreate, string>(GetRemoteUrl("Users"), new UserForCreate()
            {
                EmailAddress = emailAddress,
                FirstName = firstName,
                Password = password,
                Surname = surname
            }, _jwtToken);
            return string.Empty;
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

        public UserModel GetUser(long id)
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

        protected string GetRemoteUrl(string url, params string[] parameters)
        {
            var p = string.Join("/", parameters);
            if (parameters.Any())
            {
                return $"{_configuration.Value.ApiUrl}/{url}/{p}";
            }
            return $"{_configuration.Value.ApiUrl}/{url}";
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

        public UserPhonebook UpdateUserPhoneBook(UserPhonebook phoneBook)
        {
            return _messager.Put<UserPhonebook, UserPhonebook>(GetRemoteUrl("PhoneBook", phoneBook.Id.ToString()),
                        phoneBook, _jwtToken).Result;
        }

        public Contact UpdateContact(Contact contact)
        {
            return _messager.Put<Contact, Contact>(GetRemoteUrl("Contact", contact.Id.ToString()),
                      contact, _jwtToken).Result;
        }

        public TelephoneNumber UpdateTelephoneNumber(TelephoneNumber telephoneNumber)
        {
            return _messager.Put<TelephoneNumber, TelephoneNumber>(GetRemoteUrl("Telephone", telephoneNumber.Id.ToString()),
                telephoneNumber, _jwtToken).Result;
        }
    }
}
