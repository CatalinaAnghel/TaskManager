using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.DataModels;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.Repositories;
using TaskManager.DataAccess.Repositories.Abstractions;

namespace TaskManager.ApplicationLogic.Services
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
