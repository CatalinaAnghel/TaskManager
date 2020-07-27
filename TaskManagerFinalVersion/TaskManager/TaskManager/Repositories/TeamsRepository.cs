using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Repositories;
using TaskManager.Interfaces.Repositories;

namespace TaskManager.Repositories
{
    public class TeamsRepository : RepositoryBase<Teams>, ITeamsRepository
    {
        public TeamsRepository(TaskManagerDbContext repositoryContext)
               : base(repositoryContext)
        {
        }

        public List<Teams> FindTeamsByPM(Users user)
        {
            var teams = ( from t in RepositoryContext.Teams               
                        join ut in RepositoryContext.UserTeams on t.TeamsId equals ut.TeamsId
                         where ut.UsersId.Equals(user.Id) && ut.Job.Equals("PM")
                         select t).ToList(); 
            return teams;
        }
        
    }
}
