using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface ILoginService
    {
        public Task<SignInResult> Login(string email, string password, bool rememberMe);
    }
}
