using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Repositories;
using PhoneCall.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace PhoneCall.API.Persistence.Repositories{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context):base(context){}
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);       
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.Users
                        .Include(c=>c.Contacts)
                        .FirstOrDefaultAsync(u=>u.ID==id);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.Include(c=>c.Contacts).ToListAsync();        
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public void Update(User user)
        {
            _context.Update(user);
        }

       
    }
}