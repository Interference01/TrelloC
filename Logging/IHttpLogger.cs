using System;
using TrelloC.Helpers;

namespace TrelloC.Logging
{
    public interface IHttpLogger
    {
        void Log(HttpContext context);
    }

}
