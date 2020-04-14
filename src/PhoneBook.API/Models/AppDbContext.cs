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
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<UserPhoneBook>()
            .HasOne(p => p.User)
            .WithMany(b => b.PhoneBooks);
            
            modelBuilder.Entity<Contact>()
            .HasOne(p => p.PhoneBook)
            .WithMany(b => b.Contacts);


            modelBuilder.Entity<TelephoneNumber>()
            .HasOne(p => p.Contact)
            .WithMany(b => b.PhoneNumbers);

            modelBuilder.Entity<User>()
            .HasMany(p => p.PhoneBooks)
            .WithOne(u => u.User);

            modelBuilder.Entity<UserRole>()
                   .HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(p => p.RoleId);


            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(p => p.UserId);
        }
    }
}