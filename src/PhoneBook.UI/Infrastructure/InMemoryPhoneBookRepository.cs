using PhoneBook.UI.Models;
using PhoneBook.UI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure
{
    public class InMemoryPhoneBookRepository : IPhoneBookRepository
    {
        List<UserModel> _users;
        List<UserPhonebook> _phoneBooks;
        List<Contact> _contacts;        
        public InMemoryPhoneBookRepository()
        {
            SeedDatabase();
        }
        public string CreateUser(string firstName, string surname, string password, string emailAddress)
        {
            _users.Add(
                new UserModel
                {
                    EmailAddress = emailAddress,
                    Surname = surname,
                    FirstName = firstName,
                    Password = password
                }
                );
            return emailAddress;
        }

        public bool DoesUserExist(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUser(long id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UserPhonebook> GetUserPhoneBooks(int userId)
        {
            return _phoneBooks.Where(x => x.UserId == userId);
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return _users.OrderBy(x => x.Surname);
        }

        public AuthenticatedUser LoginUser(string emailAddress, string passwordHash)
        {
            throw new NotImplementedException();
        }
        private string GetRandomName()
        {
            var names = new string[] { "Mark", "Matthew", "John", "Roman", "Peter", "Steven", "George" };
            return names.ElementAt(new Random().Next(0, names.Length));
        }
        private string GetRandomSurname()
        {
            var names = new string[] { "Griffith", "Koopman", "Devreau", "Channing", "Irving", "Ecco", "Valencia", "Stephenson", "Pauli" };
            return names.ElementAt(new Random().Next(0, names.Length));
        }
        private List<TelephoneNumber> GenerateRandomPhoneNumbers(int ownerId, int maxCount = 5)
        {
            var result = Enumerable.Range(1, new Random().Next(maxCount)).Select(x =>
               new TelephoneNumber()
               {
                   Id = ownerId * 1000 + x,
                   ContactId = ownerId,
                   NumberType = Enum.GetValues(typeof(PhoneNumberType))
                               .Cast<PhoneNumberType>()
                               .ElementAt(new Random()
                               .Next(0, Enum.GetValues(typeof(PhoneNumberType)).Length)),
                   Number = string.Join("", Enumerable.Range(0, 10).Select(x => new Random().Next(0, 9)))
               }
            ); 
            return result.ToList();
        }
        private List<Contact> GenerateContacts(UserPhonebook phoneBook, int maxCount = 10)
        {
            return Enumerable.Range(0, maxCount).ToArray().Select(x =>
            {
                //create the contact numbers                
                var c = new Contact()
                {
                    FirstName = GetRandomName(),
                    Lastname = GetRandomSurname(),                   
                    PhoneBookId = phoneBook.Id,
                    PhoneBook = phoneBook,
                    Id = phoneBook.Id*1000 + x 
                };
                c.PhoneNumbers = GenerateRandomPhoneNumbers(c.Id, 5);
                return c;
            }).ToList();
        }
        private string GetPhoneBookName(UserModel user)
        {
            return $"{user.FirstName} - {user.Surname} - {new Random().Next(1200, 5000)}";
        }
        private void SeedDatabase()
        {
            _phoneBooks = new List<UserPhonebook>();            
            _contacts = new List<Contact>();
            _users = new List<UserModel>()
            {
            new UserModel()
            {
                Id= 1,
                FirstName ="Mark",
                Surname = "Hamilton",
                EmailAddress = "mark@ca.com",
                Password  = "12345"
            },
            new UserModel()
            {
                Id= 2,
                FirstName = "Paul",
                Surname = "Bonsafta",
                EmailAddress = "paul@t.com",
                Password = "22345"
            },
            new UserModel()
            {
                Id= 3,
                FirstName = "Matthew",
                Surname = "De Vries",
                EmailAddress = "matt@e.com",
                Password = "12445"
            },
            new UserModel()
            {
                Id= 4,
                FirstName = "Job",
                Surname = "Dangle",
                EmailAddress = "a.com",
                Password = "12443"
            }};

            /////
            _users.ForEach(x =>
            {
                var phoneBookId = x.Id * 1000;
                _phoneBooks.Add(new UserPhonebook()
                {
                    User = x,
                    UserId = x.Id,
                    Name = GetPhoneBookName(x),
                    Id = phoneBookId //add a phoneBook
                });
                var pb = _phoneBooks.Where(x => x.Id == phoneBookId).First();
                pb.Contacts = GenerateContacts(pb, 15); //add contacts to this phonwbook
                _contacts.AddRange(pb.Contacts);
            }
            );
            _phoneBooks.ForEach(p =>
            {
                var user = GetUser(p.UserId);
                user.PhoneBooks = new List<UserPhonebook>() { p };
            });
        }
        public UserPhonebook GetPhoneBook(int id)
        {
            return _phoneBooks.First(x => x.Id == id);
        }

        public Contact GetContact(int id)
        {
            return _phoneBooks.SelectMany(p=>p.Contacts).First(x=>x.Id==id);
        }

        public TelephoneNumber GetTelephoneNumber(int id)
        {
            return _phoneBooks.SelectMany(c => c.Contacts).SelectMany(t => t.PhoneNumbers).First(x => x.Id == id);
        }

        public int CreateContact(int phoneBookId, string firstName, string lastName, string phoneNumber, PhoneNumberType phoneNumberType)
        {
            var pb = _phoneBooks.First(x => x.Id == phoneBookId);
            var id = _contacts.Count(x=>x.PhoneBookId==phoneBookId) + 1;
            var contact = new Contact
            {
                Id = id,
                FirstName = firstName,
                Lastname = lastName,
                PhoneBookId = phoneBookId,
                PhoneBook = pb
            };
            var numbers = contact.PhoneNumbers.ToList();

            numbers.Add(new TelephoneNumber()
            {
                Id = contact.Id * 1000 + numbers.Count() + 1,
                ContactId = contact.Id,
                Number = phoneNumber,
                NumberType = phoneNumberType
            }); ;
            contact.PhoneNumbers = numbers;
            var contacts = pb.Contacts.ToList();
            contacts.Add(contact);
            pb.Contacts = contacts; 
            return id;
        }

        public int CreatePhoneNumber(int contactId, string phoneNumber, PhoneNumberType numberType)
        {
            var contact = _contacts.First(x => x.Id == contactId);
            List<TelephoneNumber> numbers = contact.PhoneNumbers.ToList();
            numbers.Add(new TelephoneNumber()
            {
                Id = contactId * 1000 + numbers.Count + 1,
                ContactId = contactId,
                Number = phoneNumber,
                NumberType = numberType
            }); ;
            contact.PhoneNumbers = numbers;
            return contact.PhoneNumbers.Max(c => c.Id);
        }

        public void SetAuthKey(string jwtToken)
        {
            throw new NotImplementedException();
        }

        public UserPhonebook CreatePhoneBook(int userId, string phoneBookName)
        {
            throw new NotImplementedException();
        }

        public UserPhonebook UpdateUserPhoneBook(UserPhonebook phoneBook)
        {
            throw new NotImplementedException();
        }

        public Contact UpdateContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public TelephoneNumber UpdateTelephoneNumber(TelephoneNumber telephoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
