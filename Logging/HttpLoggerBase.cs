namespace TrelloC.Logging
{
    public abstract class HttpLoggerBase : IHttpLogger
    {
        public abstract void Log(HttpContext context);
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
