using System.Collections.Generic;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.UnitOfWork;

namespace TaskManager.ApplicationLogic.Services
{
    public class UserTeamsService: IUserTeamsService
    {
        private IUnitOfWork UnitOfWork { get; }

        public UserTeamsService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        public void AddUserInTeam(UserTeams userTeam)
        {
            UnitOfWork.UserTeamsRepository.Create(userTeam);
            UnitOfWork.Complete();
        }

        public void DeleteUserFromTeam(UserTeams userTeam)
        {
            var projectTasks = UnitOfWork.ProjectTasksRepository.SeeTasks(userTeam.UsersId);
            // delete the tasks from the current project
            foreach(var task in projectTasks)
            {
                if(task.ProjectId == userTeam.Team.ProjectId)
                {
                    UnitOfWork.ProjectTasksRepository.Delete(task);
                    UnitOfWork.Complete();
                }
            }
            var foundUserTeam = UnitOfWork.UserTeamsRepository
                .FindByCondition(ut => ut.User.UserName == userTeam.UsersId &&
                                ut.TeamsId == userTeam.TeamsId);
            if(foundUserTeam != null)
            {
                UnitOfWork.UserTeamsRepository.Delete(foundUserTeam);
                UnitOfWork.Complete();
            }
            
        }

        public List<Users> GetTeamColleagues(int id, Users user)
        {
             return UnitOfWork.UserTeamsRepository.GetColleagues(id, user);

        }
    }
}
