using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IBadgesRepository : IRepositoryBase<Badges>
    {
        public Badges GetBadge(Users user);
        public bool BadgeExists(int id);
    }
}
