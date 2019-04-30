using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public abstract class OmniaApiBaseController<T> : ControllerBase
    {
        ILogger<T> _logger;
        public OmniaApiBaseController(ILogger<T> logger){
                    _logger = logger;
            }
    }