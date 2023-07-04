using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Traceability.Example.AspNETCore.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task Get()
        {
            var correlationId = HttpContext.Request.Headers["X-Correlation-ID"].FirstOrDefault();
            //ya da
            correlationId = HttpContext.Items["CorrelationId"].ToString();

            NLog.MappedDiagnosticsContext.Set("CorrelationId", correlationId);
            _logger.LogDebug("ValuesController Log");
        }
    }
}
