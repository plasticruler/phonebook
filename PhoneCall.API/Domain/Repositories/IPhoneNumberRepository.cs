using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneCall.API.Domain.Models;

namespace PhoneCall.API.Domain.Repositories
{
    public interface IPhoneNumberRepository:IRepository<PhoneNumber>{
     Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByUserIdAsync(int userId);   
    }    
}