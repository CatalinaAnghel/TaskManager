using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.Repositories.Abstractions;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface IUserTeamsService
    {
        public void AddUserInTeam(UserTeams userTeam);
        public void DeleteUserFromTeam(UserTeams userTeam);
        public List<Users> GetTeamColleagues(int id, Users user);
    }
}
