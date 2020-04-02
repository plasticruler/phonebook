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
    public class PhoneNumbersController : ControllerBase
    {
        private readonly IPhoneNumberService _phoneNumberService;
        private ILogger<PhoneNumbersController> _logger;
        private readonly IMapper _mapper;
        public PhoneNumbersController(IPhoneNumberService service, ILogger<PhoneNumbersController> logger, IMapper mapper)
        {
            _phoneNumberService = service;            
            _logger = logger;            
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<PhoneNumber>> GetAllAsync(){
            var phoneNumbers = await _phoneNumberService.PhoneNumbersAsync();
            return phoneNumbers;
        }
        [HttpGet("{userId}")]
        public async Task<IEnumerable<PhoneNumber>> GetByUserIdAsync(int userId){
            var phoneNumbers = await _phoneNumberService.GetPhoneNumbersByUserIdAsync(userId);
            return phoneNumbers;
        }
        [HttpDelete("{phoneNumberId}")]
        public async Task DeleteByIdAsync(int phoneNumberId){
            await _phoneNumberService.DeleteAsync(phoneNumberId);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsyc([FromBody] SavePhoneNumberResource resource){
            //security hole here..
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var pn = _mapper.Map<SavePhoneNumberResource, PhoneNumber>(resource);     
            if (resource.ID > 0){
                var result = await _phoneNumberService.UpdateAsync(pn.ID, pn);
                if (!result.Success)
                    return BadRequest(result.Message);            
            }
            else{
                var result = await _phoneNumberService.SaveAsync(pn);
                if (!result.Success)
                    return BadRequest(result.Message);            
            }
            
            return CreatedAtAction(nameof(GetByUserIdAsync),new {userId=resource.UserID},null);
        }
    }
}