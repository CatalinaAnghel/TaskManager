using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface ISignInService
    {
        public Task<IdentityResult> Register(string username, string password, string email, bool emailConfirmed, string firstName, string lastName, int score, List<IFormFile> profileImage);
    }
}
