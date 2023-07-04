using Microsoft.Extensions.Primitives;

namespace Traceability.Example.AspNETCore.App.Middlewares
{
    public class OtherMiddleware
    {
        private readonly RequestDelegate _next;

        public OtherMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<OtherMiddleware> _logger)
        {
            var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault();
            //ya da
            correlationId = context.Items["CorrelationId"].ToString();

            NLog.MappedDiagnosticsContext.Set("CorrelationId", correlationId);
            _logger.LogDebug("OtherMiddleware Log");

            await _next(context);
        }
    }
}
