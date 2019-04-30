using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Repositories;
using PhoneCall.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PhoneCall.API.Persistence.Repositories{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        public ContactRepository(AppDbContext context):base(context)
        {            
        }
        public async Task AddAsync(Contact item)
        {
            await _context.Contacts.AddAsync(item);        }

        public async Task<Contact> FindByIdAsync(int id)
        {
            return await _context.Contacts
                                    .Include(p=>p.PhoneNumbers)
                                    .FirstOrDefaultAsync(p=>p.ID == id);

        }

        public async Task<IEnumerable<Contact>> GetContactsByUserIdAsync(int userId)
        {
            return await _context.Contacts.Where(p=>p.UserId==userId).ToListAsync();
        }

        public async Task<IEnumerable<Contact>> ListAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public void Remove(Contact item)
        {
           _context.Contacts.Remove(item);
        }

        public void Update(Contact item)
        {
            _context.Contacts.Update(item);
        }
    }
}