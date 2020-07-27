using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IUserTeamsRepository : IRepositoryBase<UserTeams>
    {
        public List<Users> GetColleagues(int id, Users user);
    }
}
