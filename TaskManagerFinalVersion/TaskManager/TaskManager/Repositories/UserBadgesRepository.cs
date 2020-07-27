using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Repositories
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
