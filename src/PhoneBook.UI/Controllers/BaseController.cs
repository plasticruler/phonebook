using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PhoneBook.UI.Configuration;
using PhoneBook.UI.Infrastructure.Messager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhoneBook.UI.Controllers
{
    public class BaseController:Controller
    {
        protected readonly IOptionsSnapshot<AppSettings> _appSettings;

        protected readonly IMessager _messager;
        protected readonly IHttpContextAccessor _contextAccessor;
        public BaseController(IOptionsSnapshot<AppSettings> appSettings, IMessager messager, IHttpContextAccessor contextAccessor)
        {
            _appSettings = appSettings;
            _messager = messager;
            _contextAccessor = contextAccessor;            
        }
        protected new HttpContext HttpContext
        {
            get
            {
                return _contextAccessor.HttpContext;
            }
        }
        protected long? UserId
        {
            get
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    return null;
                }
                else
                {
                    return long.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                }
            }
        }
    
    }
}
