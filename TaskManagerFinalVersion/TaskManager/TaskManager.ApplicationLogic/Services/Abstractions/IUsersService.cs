using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface IUsersService
    {
        public void UpdateUser(Users user, string firstName, string lastName, string newPhoneNumber, List<IFormFile> profileImage);
        public Task<Users> GetCurrentUser(ClaimsPrincipal claims);
        public IQueryable<Users> FindAll();
        public Users FindByCondition(Expression<Func<Users, bool>> expression);
        public List<Users> FindColleagues(Users user);
    }
}
