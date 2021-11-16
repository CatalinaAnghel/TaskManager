using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.DataAccess.Repositories.Abstractions
{
    public interface IUserBadgesRepository : IRepositoryBase<UserBadges>
    {
        public List<UserBadges> FindBadges(Users user);
    }
}
