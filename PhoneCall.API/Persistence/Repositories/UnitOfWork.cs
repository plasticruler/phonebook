using System.Threading.Tasks;
using PhoneCall.API.Domain.Repositories;
using PhoneCall.API.Persistence.Contexts;

namespace PhoneCall.API.Persistence.Repositories{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;     
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}