using Microsoft.EntityFrameworkCore;
using PhoneCall.API.Domain.Enums;
using PhoneCall.API.Domain.Models;

namespace PhoneCall.API.Persistence.Contexts{
    
    public class AppDbContext:DbContext{
        public DbSet<User> Users{get;set;}
        public DbSet<Contact> Contacts{get;set;}
        public DbSet<PhoneNumber> PhoneNumbers{get;set;}
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){

        }
        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p=>p.ID);
            builder.Entity<User>().HasIndex(p=>p.EmailAddress).IsUnique();
            builder.Entity<User>().Property(p=>p.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p=>p.EmailAddress).IsRequired().HasMaxLength(100);
            builder.Entity<User>().HasMany(p=>p.Contacts).WithOne(p=>p.User).HasForeignKey(p=>p.UserId);



            builder.Entity<User>().HasData(
                new User{EmailAddress="max@abc.com",ID=101,PasswordHash="12345"},
                new User{EmailAddress="maxine@abc.com", ID=102, PasswordHash="123456"});

            builder.Entity<Contact>().ToTable("Contacts");
            builder.Entity<Contact>().HasKey(p=>p.ID);
            builder.Entity<Contact>().Property(p=>p.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Contact>().Property(p=>p.Surname).IsRequired().HasMaxLength(100);
            builder.Entity<Contact>().Property(p=>p.FirstName).HasMaxLength(100);            
            builder.Entity<Contact>().HasMany(p=>p.PhoneNumbers).WithOne(p=>p.Contact).HasForeignKey(p=>p.ContactId);

            builder.Entity<Contact>().HasData(
                new Contact{ID=401, UserId=101, FirstName="Rami", Surname="Malek"},
                new Contact{ID=402, UserId=101, FirstName="Imar", Surname="Tass"},
                new Contact{ID=403, UserId=101, FirstName="Rich", Surname="Ricardo"},
                new Contact{ID=404, UserId=101, FirstName="Tamlyn", Surname="Abrahams"},
                new Contact{ID=405, UserId=101, FirstName="Paul", Surname="Redmon"},
                new Contact{ID=406, UserId=101, FirstName="David", Surname="Seal"}
            );

            builder.Entity<PhoneNumber>().ToTable("PhoneNumbers");
            builder.Entity<PhoneNumber>().HasKey(p=>p.ID);
            builder.Entity<PhoneNumber>().Property(p=>p.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PhoneNumber>().Property(p=>p.Number).IsRequired().HasMaxLength(20);
            builder.Entity<PhoneNumber>().Property(p=>p.PhoneNumberType).HasMaxLength(10);  
            
            builder.Entity<PhoneNumber>().HasData(
             new PhoneNumber{ID=1,ContactId=401, Number="+27920000123",PhoneNumberType=EPhoneNumberType.Home, UserId=101},
             new PhoneNumber{ID=2,ContactId=401, Number="+27920000124",PhoneNumberType=EPhoneNumberType.Work, UserId=101},
             new PhoneNumber{ID=3,ContactId=401, Number="+27920000125",PhoneNumberType=EPhoneNumberType.Mobile_1, UserId=101},
             new PhoneNumber{ID=4,ContactId=406, Number="+27921000123",PhoneNumberType=EPhoneNumberType.Home, UserId=101},
             new PhoneNumber{ID=5,ContactId=406, Number="+27921000124",PhoneNumberType=EPhoneNumberType.Home, UserId=101}
            );     
        }        
    }
}