using Microsoft.Extensions.Options;
using System.Collections.Specialized;

namespace TrelloC.Logging
{

    public class HttpLoggerConsole : HttpLoggerBase
    {

        public HttpLoggerConsole(IOptions<LoggingCustomSettings> settings) : base(settings)
        {
        }
        protected override string EnableSettingName => "LogToConsole";
        protected override void LogExecuting(HttpContext context)
        {
            Console.WriteLine(GetRequestInfo(context.Request) + GetResposeInfo(context.Response));
        }
    }
}
