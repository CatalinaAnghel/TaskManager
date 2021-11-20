using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TaskManager.ApplicationLogic.Services.Abstractions;

namespace TaskManager.ApplicationLogic.Services
{
    public class SessionService: ISessionService
    {
        public void InitializeSession(HttpContext httpContext)
        {
            int number = 0;
            httpContext.Session.SetString("DashboardVisits", JsonConvert.SerializeObject(number));
        }

        public void CountVisit(HttpContext httpContext)
        {
            var number = httpContext.Session.GetString("DashboardVisits") + 1;
            httpContext.Session.SetString("DashboardVisits", JsonConvert.SerializeObject(number));
        }

        public string GetSessionValue(HttpContext httpContext, string key)
        {
            return httpContext.Session.GetString(key);
        }

    }
}
