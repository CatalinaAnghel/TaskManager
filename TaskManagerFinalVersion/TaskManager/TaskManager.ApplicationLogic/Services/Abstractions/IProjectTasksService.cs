using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.DataAccess.Repositories.Abstractions;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.Dtos;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface IProjectTasksService
    {
        IProjectTasksRepository ProjectTasksRepository { get; }
        ITeamsRepository TeamsRepository { get; }
        IUserTeamsRepository UserTeamsRepository { get; }


        public void AddTask(ProjectTasks task);
        public void DeleteTask(TasksViewModel model);
        public void UpdateTask(ProjectTasks task);
        public int GetNumberOfUrgentTasks(string userId);
        public int GetNumberOfFinishedTasks(string userId);
        public int GetNumberOfUnfinishedTasks(string userId);
        public List<ProjectTasks> FindAllByProject(int id);
        public bool ProjectTasksExists(int id);
        public List<ProjectTasks> FindAllByUserIdOrPM(string userid);
        public void AssignTaskToUser(int projectId, string taskName, string userId);

        public List<ProjectTasks> SeeTasks(string userId);
        public ProjectTasks FindByCondition(Expression<Func<ProjectTasks, bool>> expression);
        public List<ProjectTasks> FindAll(Users user);
        public TasksViewModel GetTaskViewModel(string userId);
    }
}
