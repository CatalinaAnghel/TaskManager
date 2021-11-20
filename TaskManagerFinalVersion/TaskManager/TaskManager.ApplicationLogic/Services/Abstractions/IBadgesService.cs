using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.DataAccess.Repositories.Abstractions;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface IBadgesService
    {
        public void AddBadge(Badges badge);

        public void DeleteBadge(Badges badge);

        public void UpdateBadge(Badges badge);
        public Badges FindBadgesByCondition(Expression<Func<Badges, bool>> expression);
        public bool BadgeExists(int id);
        public List<Badges> FindAllBadges();
    }
}
