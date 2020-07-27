using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
    public interface IBadgesService
    {
        IBadgesRepository BadgesRepository { get; }

        public void AddBadge(Badges badge);

        public void DeleteBadge(Badges badge);

        public void UpdateBadge(Badges badge);
        public Badges FindBadgesByCondition(Expression<Func<Badges, bool>> expression);
        public bool BadgeExists(int id);
        public List<Badges> FindAllBadges();
    }
}
