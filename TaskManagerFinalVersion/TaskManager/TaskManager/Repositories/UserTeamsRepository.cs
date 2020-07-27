using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Interfaces.Repositories;

namespace TaskManager.Repositories
{
    public class UserTeamsRepository : RepositoryBase<UserTeams>, IUserTeamsRepository
    {
        public UserTeamsRepository(TaskManagerDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public List<Users> GetColleagues(int id, Users user)
        {
            return RepositoryContext.UserTeams.Where(ut => ut.TeamsId == id).Where(ut => ut.UsersId != user.Id).Select(ut => ut.User).ToList();
        }
    }
}
