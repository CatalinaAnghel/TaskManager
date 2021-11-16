using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Repositories.Abstractions;

namespace TaskManager.DataAccess.Repositories
{
    public class UserBadgesRepository : RepositoryBase<UserBadges>, IUserBadgesRepository
    {
        public UserBadgesRepository(TaskManagerDbContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public List<UserBadges> FindBadges(Users user)
        {
            return RepositoryContext.UserBadges.Where(ub => ub.UsersId == user.Id).Include(ub => ub.Badge).OrderBy(ub => ub.Badge.NecessaryScore).ToList();
        }
    }
}
