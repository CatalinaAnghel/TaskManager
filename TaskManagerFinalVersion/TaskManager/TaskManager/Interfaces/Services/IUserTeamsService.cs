using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
    public interface IUserTeamsService
    {
        public void AddUserInTeam(UserTeams userTeam);
        public void DeleteUserFromTeam(UserTeams userTeam);
        public List<Users> GetTeamColleagues(int id, Users user);
    }
}
