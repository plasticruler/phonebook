using Microsoft.EntityFrameworkCore;
namespace PhoneBook.API.Models
{
    public class AppDbContext:DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){

        }
        public DbSet<User> Users{get;set;}
        public DbSet<Contact> Contacts{get;set;}
        public DbSet<UserPhoneBook> PhoneBook{get;set;}
        public DbSet<TelephoneNumber> TelephoneNumber{get;set;}
    }
}