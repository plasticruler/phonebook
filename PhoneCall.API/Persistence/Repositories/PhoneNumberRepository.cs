using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Repositories;
using PhoneCall.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PhoneCall.API.Persistence.Repositories{
    public class PhoneNumberRepository : BaseRepository, IPhoneNumberRepository
    {
        public PhoneNumberRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PhoneNumber item)
        {
            await _context.PhoneNumbers.AddAsync(item);
        }

        public async Task<Domain.Models.PhoneNumber> FindByIdAsync(int id)
        {
           return await _context.PhoneNumbers
                    .Include(p=>p.Contact)   
                    .FirstOrDefaultAsync(p=>p.ID==id);                            
        }

        public async Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByUserIdAsync(int userId)
        {
            return await _context.PhoneNumbers.Where(p=>p.UserId==userId).ToListAsync();
        }

        public async Task<IEnumerable<PhoneNumber>> ListAsync()
        {
            return await _context.PhoneNumbers                    
                    .ToListAsync();
        }

        public void Remove(PhoneNumber item)
        {
            _context.PhoneNumbers.Remove(item);
        }

        public void Update(PhoneNumber item)
        {
            _context.PhoneNumbers.Update(item);
        }
    }
}