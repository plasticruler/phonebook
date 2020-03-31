using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Repositories;
using PhoneCall.API.Domain.Services;
using PhoneCall.API.Domain.Services.Communication;

namespace PhoneCall.API.Services
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private IPhoneNumberRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PhoneNumberService(IPhoneNumberRepository phoneNumberRepository, IUnitOfWork unitOfWork)
        {
            _repository = phoneNumberRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<ApiResponse<PhoneNumber>> DeleteAsync(int id)
        {
            var item = await _repository.FindByIdAsync(id);

            if (item == null)
                return new ApiResponse<PhoneNumber>("Number not found.");

            try
            {
                _repository.Remove(item);
                await _unitOfWork.CompleteAsync();

                return new ApiResponse<PhoneNumber>(item);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ApiResponse<PhoneNumber>($"An error occurred when deleting the number: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PhoneNumber>> PhoneNumbersAsync()
        {
            return await _repository.ListAsync();
        }

        public async Task<ApiResponse<PhoneNumber>> SaveAsync(PhoneNumber phoneNumber)
        {
            try
            {
                await _repository.AddAsync(phoneNumber);
                await _unitOfWork.CompleteAsync();
                return new ApiResponse<PhoneNumber>(phoneNumber);

            }
            catch (Exception ex)
            {
                return new ApiResponse<PhoneNumber>($"There was an error saving the number. {ex.Message}");
            }
        }

        public async Task<ApiResponse<PhoneNumber>> UpdateAsync(int id, PhoneNumber phoneNumber)
        {
            var item = await _repository.FindByIdAsync(id);
            if (item == null)
                return new ApiResponse<PhoneNumber>("Number not found.");
            item.PhoneNumberType = phoneNumber.PhoneNumberType;
            item.Number = phoneNumber.Number;

            try
            {
                _repository.Update(item);
                await _unitOfWork.CompleteAsync();
                return new ApiResponse<PhoneNumber>(item);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PhoneNumber>($"An error occurred when updating the number: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByUserIdAsync(int userId)
        {
            return await _repository.GetPhoneNumbersByUserIdAsync(userId);
        }
    }
}