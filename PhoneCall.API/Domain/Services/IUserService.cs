using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Services.Communication;

namespace PhoneCall.API.Domain.Services
{
    public interface IUserService{
        Task<IEnumerable<User>> ListAsync();

         Task<ApiResponse<User>> SaveAsync(User user);
        Task<ApiResponse<User>> UpdateAsync(int id, User user);
        Task<ApiResponse<User>> DeleteAsync(int id);
        
    }
}