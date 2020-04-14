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
using Microsoft.IdentityModel.Tokens;
using PhoneBook.API.Models;
using PhoneBook.API.Models.DTO;

namespace PhoneBook.API.Controllers
{   
    public class LoginController : BaseController
    {
        private IConfiguration _config;
        private readonly AppDbContext _context;      
        public LoginController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserForAuthentication<long> login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
           
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              GetClaims(userInfo),
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserForAuthentication<long> AuthenticateUser(UserForAuthentication<long> login)
        {
            UserForAuthentication<long> user = null;
            var userAccount = _context.Users.FirstOrDefault(u => u.EmailAddress == login.EmailAddress && u.PasswordHash == login.PasswordHash);
              
            if (userAccount != null)
            {
                user = new UserForAuthentication<long> {  EmailAddress = userAccount.EmailAddress, Id = userAccount.Id };
            }
            return user;
        }
    }
}