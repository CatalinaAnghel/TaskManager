using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Repositories;
using TaskManager.DataAccess.Repositories.Abstractions;

namespace TaskManager.ApplicationLogic.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<Users> _userManager;
        private readonly IImageService _imageService;

        private IUsersRepository UsersRepository { get; }

        public UsersService( UserManager<Users> userManager, TaskManagerDbContext context, IImageService imageService)
        {
            _userManager = userManager;
            UsersRepository = new UsersRepository(context);
            _imageService = imageService;
        }

        // it does not work
        public async void UpdateUser(Users user, string firstName, string lastName, string newPhoneNumber, List<IFormFile> profileImage)
        {
            if (user.FirstName != firstName)
            {
                user.FirstName = firstName;
            }
            if (user.LastName != lastName)
            {
                user.LastName = lastName;
            }

            if (profileImage != null)
            {
                _imageService.SetProfileImage(profileImage, user);
            }
            

            var result = await _userManager.UpdateAsync(user);

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (newPhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, newPhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
        }

        public async Task<Users> GetCurrentUser(ClaimsPrincipal claims)
        {
            return await _userManager.GetUserAsync(claims);
        }

        public IQueryable<Users> FindAll()
        {
            return _userManager.Users;
        }

        public Users FindByCondition(Expression<Func<Users, bool>> expression)
        {
            return _userManager.Users.Where(expression).SingleOrDefault();
        }

        public List<Users> FindColleagues(Users user)
        {
            return UsersRepository.FindColleagues(user);
        }
    }
}
