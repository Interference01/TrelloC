using Microsoft.Extensions.Options;

namespace TrelloC.Logging
{
    public class HttpLoggerFile : HttpLoggerBase
    {
        public HttpLoggerFile(IOptions<LoggingCustomSettings> settings) : base(settings)
        {
        }
        protected override string EnableSettingName => "LogToFile";
        protected override void LogExecuting(HttpContext context)
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
