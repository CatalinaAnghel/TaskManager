using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.Repositories.Abstractions;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface IUserBadgesService
    {
        IUserBadgesRepository UserBadgesRepository { get; }

        public List<UserBadges> FindBadges(Users user);
    }
}
