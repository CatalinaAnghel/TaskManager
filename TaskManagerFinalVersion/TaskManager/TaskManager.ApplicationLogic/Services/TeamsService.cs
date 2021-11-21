using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskManager.DataAccess.DataModels;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.UnitOfWork;

namespace TaskManager.ApplicationLogic.Services
{
    public class TeamsService: ITeamsService
    {
        public IUnitOfWork UnitOfWork { get; }
        public TeamsService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void AddTeam(Users user, Teams team)
        {
            UserTeams ut = new UserTeams
            {
                UsersId = user.Id,
                Team = team,
                Job = "PM"
            };

            UnitOfWork.TeamsRepository.Create(team);
            UnitOfWork.UserTeamsRepository.Create(ut);
            UnitOfWork.Complete();
        }

        public void DeleteTeam(Teams team)
        {
            var foundTeam = UnitOfWork.TeamsRepository.FindByCondition(t => t.TeamsId == team.TeamsId);
            if(foundTeam != null)
            {
                UnitOfWork.TeamsRepository.Delete(foundTeam);
                UnitOfWork.Complete();
            }
        }

        public void UpdateTeam(Teams team)
        {
            var foundTeam = UnitOfWork.TeamsRepository.FindByCondition(t => t.TeamsId == team.TeamsId);
            if (foundTeam != null)
            {
                foundTeam.Name = team.Name;
                foundTeam.Project = team.Project;
                foundTeam.ProjectId = team.ProjectId;

                UnitOfWork.TeamsRepository.Update(foundTeam);
                UnitOfWork.Complete();
            }
        }

        public List<Teams> FindAll()
        {
            return UnitOfWork.TeamsRepository.FindAll();
        }

        public Teams FindByCondition(Expression<Func<Teams, bool>> expression)
        {
            return UnitOfWork.TeamsRepository.FindByCondition(expression);
        }

        public List<Teams> FindTeamsByPM(Users user)
        {
            var teams = UnitOfWork.TeamsRepository.FindTeamsByPM(user);
            return teams;
        }
    }
}
