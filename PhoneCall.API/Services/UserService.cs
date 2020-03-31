using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Repositories;
using PhoneCall.API.Domain.Services;
using PhoneCall.API.Domain.Services.Communication;

namespace PhoneCall.API.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        private IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork){
            _repository = userRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<ApiResponse<User>> DeleteAsync(int id)
        {
             var item = await _repository.FindByIdAsync(id);

            if (item == null)
                return new ApiResponse<User>("User not found.");

            try
            {
                _repository.Remove(item);
                await _unitOfWork.CompleteAsync();

                return new ApiResponse<User>(item);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ApiResponse<User>($"An error occurred when deleting the user: {ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _repository.ListAsync();
        }

        public async Task<ApiResponse<User>> SaveAsync(User user)
        {
             try
            {
                await _repository.AddAsync(user);
                await _unitOfWork.CompleteAsync();
                return new ApiResponse<User>(user);

            }
            catch (Exception ex)
            {
                return new ApiResponse<User>($"There was an error saving the user. {ex.Message}");
            }
        }

        public async Task<ApiResponse<User>> UpdateAsync(int id, User user)
        {
            var item = await _repository.FindByIdAsync(id);
            if (item == null)
                return new ApiResponse<User>("User not found.");
            item.EmailAddress = user.EmailAddress;
            item.PasswordHash= user.PasswordHash;            

            try
            {
                _repository.Update(item);
                await _unitOfWork.CompleteAsync();
                return new ApiResponse<User>(item);
            }
            catch (Exception ex)
            {
                return new ApiResponse<User>($"An error occurred when updating the user: {ex.Message}");
            }
        }

        
    }
}