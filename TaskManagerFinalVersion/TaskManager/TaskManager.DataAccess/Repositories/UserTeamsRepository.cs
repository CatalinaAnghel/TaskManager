using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Repositories.Abstractions;

namespace TaskManager.DataAccess.Repositories
{
    public class UserTeamsRepository : RepositoryBase<UserTeams>, IUserTeamsRepository
    {
        public UserTeamsRepository(TaskManagerDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public List<Users> GetColleagues(int id, Users user)
        {
            return RepositoryContext.UserTeams
                .Where(ut => ut.TeamsId == id)
                .Where(ut => ut.UsersId != user.Id)
                .Select(ut => ut.User)
                .ToList();
        }
    }
}
