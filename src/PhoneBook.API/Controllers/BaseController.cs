using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
        protected ClaimsPrincipal CurrentUser => HttpContext.User;
        
        protected long? UserId
        {
            get
            {
                if (CurrentUser == null)
                    return null;

                if (CurrentUser.HasClaim(c => c.Type == JwtRegisteredClaimNames.NameId))
                    return long.Parse(CurrentUser.FindFirstValue(JwtRegisteredClaimNames.NameId));
                
                return null;
            }
        }
        protected bool HasRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return false;

            if (CurrentUser == null)
                return false;
            
            var roles = CurrentUser.Claims.FirstOrDefault(c => c.Type == "Roles");
            if (roles == null)
                return false;
            
            return (roles.Value.ToUpper().Split('|').Contains(roleName.ToUpper()));
        }
    }
}