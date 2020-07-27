using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Interfaces.Services;

namespace TaskManager.Services
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
