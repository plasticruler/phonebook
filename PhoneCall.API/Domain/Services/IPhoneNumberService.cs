using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Services.Communication;

namespace PhoneCall.API.Domain.Services
{
    public interface IPhoneNumberService{
        Task<IEnumerable<PhoneNumber>> PhoneNumbersAsync();
        Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByUserIdAsync(int userId);
         Task<ApiResponse<PhoneNumber>> SaveAsync(PhoneNumber phoneNumber);
        Task<ApiResponse<PhoneNumber>> UpdateAsync(int id, PhoneNumber phoneNumber);
        Task<ApiResponse<PhoneNumber>> DeleteAsync(int id);
    }
}