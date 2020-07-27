using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Services
{
    public class UserBadgesService : IUserBadgesService
    {
        public IUserBadgesRepository UserBadgesRepository { get; }
        public UserBadgesService(TaskManagerDbContext context)
        {
            UserBadgesRepository = new UserBadgesRepository(context);
        }

        public List<UserBadges> FindBadges(Users user)
        {
            return UserBadgesRepository.FindBadges(user);
        }
    }
}
