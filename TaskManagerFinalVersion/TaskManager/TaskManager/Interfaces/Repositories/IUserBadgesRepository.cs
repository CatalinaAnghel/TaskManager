using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IUserBadgesRepository : IRepositoryBase<UserBadges>
    {
        public List<UserBadges> FindBadges(Users user);
    }
}
