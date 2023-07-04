using Microsoft.Extensions.Primitives;

namespace Traceability.Example.AspNETCore.App.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private const string correlationIdHeaderKey = "X-Correlation-ID";
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<CorrelationIdMiddleware> _logger)
        {
            //Request'e karşılık yeni bir correlation id üretiliyor.
            string correlationId = Guid.NewGuid().ToString();

            //Request'in header'ını kontrol ediliyor eğer ki isteğin correlation id'si varsa elde ediliyor, yoksa üretilen correlation id değeri header'a ekleniyor.
            if (context.Request.Headers.TryGetValue(correlationIdHeaderKey, out StringValues _correlationId))
                correlationId = _correlationId.ToString();
            else
                context.Request.Headers.Add(correlationIdHeaderKey, correlationId);

            //nlog.config dosyasındaki Mapped Diagnostics Context (MDC) olan @CorrelationId parametresine değeri set ediliyor.
            NLog.MappedDiagnosticsContext.Set("CorrelationId", correlationId);

            //Correlation Id üzerinden kümülatif log atılıyor.
            _logger.LogDebug("Asp.NET Core App. Log Example");

            //Bütünsel bir izlenebilirlik kazandırabilmek için correlation id response'la da ilişkilendiriliyor.
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.TryGetValue(correlationIdHeaderKey, out _))
                    context.Response.Headers.Add(correlationIdHeaderKey, correlationId);
                return Task.CompletedTask;
            });

            //Correlation ID sonraki bileşene aktarılıyor.
            context.Items["CorrelationId"] = correlationId;

            await _next(context);
        }
    }
}
