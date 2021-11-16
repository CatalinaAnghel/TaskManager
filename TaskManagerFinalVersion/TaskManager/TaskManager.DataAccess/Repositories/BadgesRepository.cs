using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.Repositories.Abstractions;
using TaskManager.DataAccess.Data;

namespace TaskManager.DataAccess.Repositories
{
    public class BadgesRepository : RepositoryBase<Badges>, IBadgesRepository
    {
        public BadgesRepository(TaskManagerDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public Badges GetBadge(Users user)
        {
            // get the user's badges:
            var badges = (from b in RepositoryContext.Badges
                          join ub in RepositoryContext.UserBadges on b.BadgesId equals ub.BadgeId
                          where ub.UsersId == user.Id
                          select b.BadgesId).ToList();

            // find the badge won by the user (he has the necessary score and he does not have the badge)
            var wonbadge = (from badge in RepositoryContext.Badges
                            from u in RepositoryContext.Users
                            where u.Score >= badge.NecessaryScore && !badges.Contains(badge.BadgesId)
                            orderby badge.NecessaryScore ascending
                            select badge).FirstOrDefault();

            return wonbadge;
        }

        public bool BadgeExists(int id)
        {
            return RepositoryContext.Badges.Any(e => e.BadgesId == id);
        }
    }
}
