using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface ISessionService
    {
        public void InitializeSession(HttpContext httpContext);
        public void CountVisit(HttpContext httpContext);
        public string GetSessionValue(HttpContext httpContext, string key);
    }
}
