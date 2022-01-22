using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TaskManager.ApplicationLogic.Dtos;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface ISignInService
    {
        public Task<IdentityResult> Register(RegisterDto registerDto);
    }
}
