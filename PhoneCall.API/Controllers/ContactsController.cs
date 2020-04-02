using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Services;
using PhoneCall.API.Extensions;
using PhoneCall.API.Resources;

namespace PhoneCall.API.Controllers
{
    [Route("api/1.0/[controller]")]  
    [Produces(MediaTypeNames.Application.Json)]  
    public class ContactsController : ControllerBase
    {
       private readonly IContactService _service;
        private ILogger<ContactsController> _logger;
        private readonly IMapper _mapper;
        public ContactsController(IContactService service, ILogger<ContactsController> logger, IMapper mapper)
        {
            _service = service;            
            _logger = logger;            
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAllAsync(){
            var contacts = await _service.ContactsAsync();
            
            return contacts;
        }
        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<Contact>> GetContactByUserIdAsync(int userId){   
            _logger.LogDebug("Recieved " + userId);
            var contacts  = await _service.GetContactsByUserIdAsync(userId);
            return contacts;
        }
        [HttpGet("{contactId}")]
        public async Task<Contact> GetContactByIdAsync(int contactId){
            var contact = await _service.GetContactById(contactId);
            return contact.Item;
        }

        [HttpDelete("{contactId}")]
        public async Task DeleteByIdAsync(int contactId ){
            
            await _service.DeleteAsync(contactId);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsyc([FromBody] SaveContactResource resource){
            //security hole here..
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var c = _mapper.Map<SaveContactResource, Contact>(resource);     
            if (resource.ID > 0){
                var result = await _service.UpdateAsync(c.ID, c);
                if (!result.Success)
                    return BadRequest(result.Message);            
            }
            else{
                var result = await _service.SaveAsync(c);
                if (!result.Success)
                    return BadRequest(result.Message);            
            }            
            return CreatedAtAction(nameof(GetContactByIdAsync), new {userId=c.ID}, c);
        }
    }
}