using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Repositories;
using PhoneCall.API.Domain.Services;
using PhoneCall.API.Domain.Services.Communication;

namespace PhoneCall.API.Services
{
    public class ContactService : IContactService
    {
        private IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<Contact>> ContactsAsync()
        {
            return await _contactRepository.ListAsync();
        }

        public async Task<ApiResponse<Contact>> SaveAsync(Contact contact)
        {
            try
            {
                await _contactRepository.AddAsync(contact);
                await _unitOfWork.CompleteAsync();
                return new ApiResponse<Contact>(contact);

            }
            catch (Exception ex)
            {
                return new ApiResponse<Contact>($"There was an error saving the contact. {ex.Message}");
            }
        }

        public async Task<ApiResponse<Contact>> UpdateAsync(int id, Contact contact)
        {
            var existingContact = await _contactRepository.FindByIdAsync(id);
            if (existingContact == null)
                return new ApiResponse<Contact>("Contact not found.");
            existingContact.FirstName = contact.FirstName;
            existingContact.Surname = contact.Surname;
            existingContact.PhoneNumbers = contact.PhoneNumbers;
            try
            {
                _contactRepository.Update(existingContact);
                await _unitOfWork.CompleteAsync();
                return new ApiResponse<Contact>(existingContact);
            }
            catch (Exception ex)
            {
                return new ApiResponse<Contact>($"An error occurred when updating the contact: {ex.Message}");
            }
        }

        public async Task<ApiResponse<Contact>> DeleteAsync(int id)
        {
            var existingContact = await _contactRepository.FindByIdAsync(id);

            if (existingContact == null)
                return new ApiResponse<Contact>("Contact not found.");

            try
            {
                _contactRepository.Remove(existingContact);
                await _unitOfWork.CompleteAsync();

                return new ApiResponse<Contact>(existingContact);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ApiResponse<Contact>($"An error occurred when deleting the cpmtact: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Contact>> GetContactsByUserIdAsync(int userId)
        {
            return await _contactRepository.GetContactsByUserIdAsync(userId);
        }

        public async Task<ApiResponse<Contact>> GetContactById(int contactId)
        {
            var existingContact = await _contactRepository.FindByIdAsync(contactId);

            if (existingContact == null)
                return new ApiResponse<Contact>("Contact not found.");
            return new ApiResponse<Contact>(existingContact);
        }
    }
}