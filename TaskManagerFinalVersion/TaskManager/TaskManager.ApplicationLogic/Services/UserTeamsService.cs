using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Repositories;
using TaskManager.DataAccess.Repositories.Abstractions;

namespace TaskManager.ApplicationLogic.Services
{
    public class UserTeamsService: IUserTeamsService
    {
        private IUserTeamsRepository UserTeamsRepository { get; }
        private IProjectTasksRepository ProjectTasksRepository { get; }

        public UserTeamsService(TaskManagerDbContext context)
        {
            UserTeamsRepository = new UserTeamsRepository(context);
            ProjectTasksRepository = new ProjectTasksRepository(context);
        }


        public void AddUserInTeam(UserTeams userTeam)
        {
            UserTeamsRepository.Create(userTeam);
            UserTeamsRepository.Save();
        }

        public void DeleteUserFromTeam(UserTeams userTeam)
        {
            var projectTasks = ProjectTasksRepository.SeeTasks(userTeam.UsersId);
            // delete the tasks from the current project
            foreach(var task in projectTasks)
            {
                if(task.ProjectId == userTeam.Team.ProjectId)
                {
                    ProjectTasksRepository.Delete(task);
                    ProjectTasksRepository.Save();
                }
            }
            var foundUserTeam = UserTeamsRepository.FindByCondition(ut => ut.User.UserName == userTeam.UsersId && ut.TeamsId == userTeam.TeamsId);
            if(foundUserTeam != null)
            {
                UserTeamsRepository.Delete(foundUserTeam);
                UserTeamsRepository.Save();
            }
            
        }

        public List<Users> GetTeamColleagues(int id, Users user)
        {
             return UserTeamsRepository.GetColleagues(id, user);

        }
    }
}
