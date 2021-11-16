using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.DataAccess.Repositories.Abstractions
{
    public interface IUserTeamsRepository : IRepositoryBase<UserTeams>
    {
        public List<Users> GetColleagues(int id, Users user);
    }
}
