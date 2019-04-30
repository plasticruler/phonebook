using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneCall.API.Domain.Models;

namespace PhoneCall.API.Domain.Repositories
{
    public interface IRepository<T>{
        Task<IEnumerable<T>> ListAsync();
        Task AddAsync(T item);
        Task<T> FindByIdAsync(int id);
        void Update(T item);
        void Remove(T item);
    }
    
}