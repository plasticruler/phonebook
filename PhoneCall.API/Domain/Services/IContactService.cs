using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Services.Communication;

namespace PhoneCall.API.Domain.Services
{
    public interface IContactService{
        Task<IEnumerable<Contact>> ContactsAsync();
        Task<ApiResponse<Contact>> SaveAsync(Contact contact);
        Task<ApiResponse<Contact>> UpdateAsync(int id, Contact contact);
        Task<ApiResponse<Contact>> DeleteAsync(int id);
        Task<IEnumerable<Contact>> GetContactsByUserIdAsync(int userId);
        Task<ApiResponse<Contact>> GetContactById(int contactId);
        
    }
}