using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface ITeamsService
    {
        public void AddTeam(Users user, Teams team);
        public void DeleteTeam(Teams team);
        public void UpdateTeam(Teams team);
        public List<Teams> FindAll();
        public Teams FindByCondition(Expression<Func<Teams, bool>> expression);
        public List<Teams> FindTeamsByPM(Users user);
    }
}
