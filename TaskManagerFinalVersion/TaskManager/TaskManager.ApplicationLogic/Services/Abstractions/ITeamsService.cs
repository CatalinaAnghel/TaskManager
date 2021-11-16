using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.DataAccess.Repositories.Abstractions;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface ITeamsService
    {
        ITeamsRepository TeamsRepository { get; }

        public void AddTeam(Users user, Teams team);
        public void DeleteTeam(Teams team);
        public void UpdateTeam(Teams team);
        public List<Teams> FindAll();
        public Teams FindByCondition(Expression<Func<Teams, bool>> expression);
        public List<Teams> FindTeamsByPM(Users user);
    }
}
