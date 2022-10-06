using Microsoft.Extensions.Options;
using TrelloC.Logging;

namespace TrelloC.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LoggingCustomSettings _logSettings;

        public LoggingMiddleware(RequestDelegate next, IOptions<LoggingCustomSettings> logSettings)
        {
            _next = next;
            _logSettings = logSettings.Value;
        }
        public async Task Invoke(HttpContext context, IEnumerable<IHttpLogger> _logs)
        {
            await _next.Invoke(context);
            foreach (var logger in _logs)
            {
                logger.Log(context);
            }
        }
    }
}
