using Microsoft.Extensions.Options;
using System.Collections.Specialized;

namespace TrelloC.Logging
{

    public class HttpLoggerConsole : HttpLoggerBase
    {
        private LoggingCustomSettings _settings;
        public HttpLoggerConsole(IOptions<LoggingCustomSettings> settings)
        {
            _settings = settings.Value;
        }

        public override void Log(HttpContext context)
        {
            if (_settings.LogToConsole)
            {
                Console.WriteLine(GetRequestInfo(context.Request) + GetResposeInfo(context.Response));
            }
        }
    }
}
