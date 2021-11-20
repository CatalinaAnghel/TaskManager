using System.Collections.Generic;
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
