using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneCall.API.Domain.Models;
using PhoneCall.API.Domain.Services;
using PhoneCall.API.Extensions;
using PhoneCall.API.Resources;

namespace PhoneCall.API.Controllers
{
    [Route("api/1.0/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, ILogger<UsersController> logger, IMapper mapper)
        {
            _userService = userService;            
            _logger = logger;            
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetAllAsync(){
            var users = await _userService.ListAsync();
            return users;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsyc([FromBody] SaveUserResource resource){
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var user = _mapper.Map<SaveUserResource, User>(resource);           
            var result = await _userService.SaveAsync(user);
            if (!result.Success)
                    return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
