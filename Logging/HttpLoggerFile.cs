using Microsoft.Extensions.Options;

namespace TrelloC.Logging
{
    public class HttpLoggerFile : HttpLoggerBase
    {
        private LoggingCustomSettings _settings;
        public HttpLoggerFile(IOptions<LoggingCustomSettings> settings)
        {
            _settings = settings.Value;
        }
        public override void Log(HttpContext context)
        {
            if (_settings.LogToFile)
            {
                try
                {
                    StreamWriter writer = new($"{Environment.CurrentDirectory}\\Logging\\txt\\Logging{DateTime.Now:MM/dd/yyyy}.txt", true);
                    writer.WriteLine(GetRequestInfo(context.Request) + GetHeadersInfo(context.Request.Headers)
                        + GetResposeInfo(context.Response) + GetHeadersInfo(context.Response.Headers) + new string('-', 40));
                    writer.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        protected string GetHeadersInfo(IHeaderDictionary headersDictionary)
        {
            string headers = "";

            foreach (var item in headersDictionary)
            {
                headers += (item.Key + ":  " + item.Value + Environment.NewLine);
            }
            return headers;
        }
    }
}
