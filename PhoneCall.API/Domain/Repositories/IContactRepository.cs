using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneCall.API.Domain.Models;

namespace PhoneCall.API.Domain.Repositories
{
    public interface IContactRepository:IRepository<Contact>{
        Task<IEnumerable<Contact>> GetContactsByUserIdAsync(int userId); 
    }
    
}