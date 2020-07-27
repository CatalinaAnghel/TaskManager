using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Services
{
    public class TeamsService: ITeamsService
    {
        public ITeamsRepository TeamsRepository { get; }
        public IUserTeamsRepository UserTeamsRepository { get; }
        public TeamsService(TaskManagerDbContext context)
        {
            TeamsRepository = new TeamsRepository(context);
            UserTeamsRepository = new UserTeamsRepository(context);
        }

        public void AddTeam(Users user, Teams team)
        {
            UserTeams ut = new UserTeams
            {
                UsersId = user.Id,
                Team = team,
                Job = "PM"
            };
            
            TeamsRepository.Create(team);
            TeamsRepository.Save();
            UserTeamsRepository.Create(ut);
            UserTeamsRepository.Save();
        }

        public void DeleteTeam(Teams team)
        {
            var foundTeam = TeamsRepository.FindByCondition(t => t.TeamsId == team.TeamsId);
            if(foundTeam != null)
            {
                TeamsRepository.Delete(foundTeam);
                TeamsRepository.Save();
            }
        }

        public void UpdateTeam(Teams team)
        {
            var foundTeam = TeamsRepository.FindByCondition(t => t.TeamsId == team.TeamsId);
            if (foundTeam != null)
            {
                foundTeam.Name = team.Name;
                foundTeam.Project = team.Project;
                foundTeam.ProjectId = team.ProjectId;
         
                TeamsRepository.Update(foundTeam);
                TeamsRepository.Save();
            }
        }

        public List<Teams> FindAll()
        {
            return TeamsRepository.FindAll();
        }

        public Teams FindByCondition(Expression<Func<Teams, bool>> expression)
        {
            return TeamsRepository.FindByCondition(expression);
        }

        public List<Teams> FindTeamsByPM(Users user)
        {
            var teams = TeamsRepository.FindTeamsByPM(user);
            return teams;
        }
    }
}
