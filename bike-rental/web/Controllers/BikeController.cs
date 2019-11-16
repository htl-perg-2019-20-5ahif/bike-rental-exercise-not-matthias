using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BikeController : ControllerBase
    {
        private readonly ILogger<BikeController> _logger;

        public BikeController(ILogger<BikeController> logger)
        {
            _logger = logger;
        }
    }
}
