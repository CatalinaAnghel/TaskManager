using System.Collections.Generic;
using System.Linq;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.Repositories.Abstractions;

namespace TaskManager.DataAccess.Repositories
{
    public class UsersRepository: RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(TaskManagerDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public List<Users> FindColleagues(Users user)
        {
            var teams = (from ut in RepositoryContext.UserTeams
                         where ut.UsersId.Equals(user.Id) && ut.Job.Equals("PM")
                         select ut.TeamsId).ToList();

            return (from u in RepositoryContext.Users
                    join ut in RepositoryContext.UserTeams on u.Id equals ut.UsersId
                    where teams.Contains(ut.TeamsId)
                    select u).Distinct().ToList();
        }

        public List<Users> FindTeamMembers (int projectId)
        {
            var users =  (from ut in RepositoryContext.UserTeams
                         join t in RepositoryContext.Teams on ut.TeamsId equals t.TeamsId
                         join u in RepositoryContext.Users on ut.UsersId equals u.Id
                         where t.ProjectId == projectId
                         select u).Distinct().ToList();
            return users;

        }

    }
}
