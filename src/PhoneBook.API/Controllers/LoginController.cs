using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhoneBook.API.Models;
using PhoneBook.API.Models.DTO;
using PhoneBook.API.Options;

namespace PhoneBook.API.Controllers
{   
    public class LoginController : BaseController
    {
        private readonly IOptionsSnapshot<AppSettings> _appSettings;
        private readonly AppDbContext _context;
        private readonly IOptionsSnapshot<JwtOptions> _jwtOptions;

        public LoginController(IOptionsSnapshot<AppSettings> appSettings, AppDbContext context,
                                IOptionsSnapshot<JwtOptions> jwtOptions)
        {
            _context = context;
            _appSettings = appSettings;
            _jwtOptions = jwtOptions;
        }
        [AllowAnonymous]
        [HttpPost]       
        public IActionResult Login([FromBody] UserForAuthentication<long> login)
        {
            IActionResult response = Unauthorized("Invalid username or password.");
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);                
                response = new JsonResult(new
                {
                    user.Id,
                    JsonToken = tokenString,
                    FirstName = user.GivenName
                });
            }

            return response;
        }
        private string GetRoles(UserForAuthentication<long> user)
        {
            var userRoles = _context.UserRoles.Where(x => x.UserId == user.Id);
            return string.Join("|", userRoles.Select(s => s.Role.RoleName));
        }
        private IEnumerable<Claim> GetClaims(UserForAuthentication<long> user){
            
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),              
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim("Roles", GetRoles(user)), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            return claims;
        }
        private string GenerateJSONWebToken(UserForAuthentication<long> userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
           
            var token = new JwtSecurityToken(_jwtOptions.Value.Issuer,
              _jwtOptions.Value.Issuer,
              GetClaims(userInfo),
              expires: DateTime.Now.AddHours(_appSettings.Value.TokenTimeOutInHours),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserForAuthentication<long> AuthenticateUser(UserForAuthentication<long> login)
        {
            UserForAuthentication<long> user = null;
            var userAccount = _context.Users.FirstOrDefault(u => u.EmailAddress == login.EmailAddress && u.PasswordHash == login.PasswordHash);
              
            if (userAccount != null)
            {
                user = new UserForAuthentication<long> {  EmailAddress = userAccount.EmailAddress, Id = userAccount.Id, GivenName = userAccount.FirstName };
            }
            return user;
        }
    }
}