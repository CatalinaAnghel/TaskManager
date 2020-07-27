using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
    public interface IUserBadgesService
    {
        IUserBadgesRepository UserBadgesRepository { get; }

        public List<UserBadges> FindBadges(Users user);
    }
}
