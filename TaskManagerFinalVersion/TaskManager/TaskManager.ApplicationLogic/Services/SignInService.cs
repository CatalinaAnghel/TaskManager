using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.ApplicationLogic.Services
{
    public class SignInService : ISignInService
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IImageService _imageService;
        

        public SignInService(UserManager<Users> userManager, RoleManager<Roles> roleManager, IImageService imageService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _imageService = imageService;
        }

        public async Task<IdentityResult> Register(string username, string password, string email, bool emailConfirmed, string firstName, string lastName, int score, List<IFormFile> profileImage)
        {
            var user = new Users
            {
                UserName = username,
                Email = email,
                EmailConfirmed = emailConfirmed,
                FirstName = firstName,
                LastName = lastName,
                Score = score
            };

            _imageService.SetProfileImage(profileImage, user);
            
            var result = await _userManager.CreateAsync(user, password);
            
            if (result.Succeeded)
            {
                var foundRole = _roleManager.Roles.Where(r => r.Name == "User").Single();
                result = await _userManager.AddToRoleAsync(user, foundRole.Name);
                
            }
            return result;
        }
    }
}
