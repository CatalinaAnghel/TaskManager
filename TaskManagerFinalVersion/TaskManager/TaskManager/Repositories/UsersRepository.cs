using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Repositories
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

    }
}
