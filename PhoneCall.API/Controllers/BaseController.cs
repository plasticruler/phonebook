using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Web;
[ApiController]
public abstract class OmniaApiBaseController<T> : ControllerBase
    {    
    ILogger<T> _logger;
        public OmniaApiBaseController(ILogger<T> logger){
                    _logger = logger;
            }
    }