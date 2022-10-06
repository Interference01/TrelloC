using Microsoft.Extensions.Options;
using TrelloC.Extensions;
namespace TrelloC.Logging
{
    public abstract class HttpLoggerBase : IHttpLogger
    {
        protected virtual string EnableSettingName { get; }
        private readonly LoggingCustomSettings _settings;
        public HttpLoggerBase(IOptions<LoggingCustomSettings> settings)
        {
            _settings = settings.Value;
        }
        public void Log(HttpContext context)
        {
            if (IsEnable())
            {
                LogExecuting(context);
            }
        }
        protected abstract void LogExecuting(HttpContext context);
        protected bool IsEnable() 
        {
            return string.IsNullOrEmpty(EnableSettingName) || (bool)_settings.GetPropertyValue<LoggingCustomSettings>(EnableSettingName);
        }
        
        protected virtual string GetResposeInfo(HttpResponse context)
        {
            string contextInfo = 
              "RESPONSE \n" +
              $"StatusCode: {context.StatusCode} \n" +
              $"ContentType: {context.ContentType} \n" +
              $"Body: {context.BodyWriter} \n";
            return contextInfo;
        }
        protected virtual string GetRequestInfo(HttpRequest context)
        {
            string contextInfo = "REQUEST \n" +
              $"{DateTime.Now} \n" +
              $"URL: {context.Path} \n" +
              $"Method: {context.Method} \n" +
              $"Body: {context.BodyReader} \n";
            return contextInfo;
        }
    }
}
