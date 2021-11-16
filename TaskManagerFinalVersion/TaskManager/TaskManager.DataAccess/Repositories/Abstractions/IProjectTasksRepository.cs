using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.Dtos;

namespace TaskManager.DataAccess.Repositories.Abstractions
{
    public interface IProjectTasksRepository : IRepositoryBase<ProjectTasks>
    {
        public List<ProjectTasks> SeeTasks(string userId);

        public int GetNumberOfUrgentTasks(string userId);
        public int GetNumberOfFinishedTasks(string userId);
        public int GetNumberOfUnfinishedTasks(string userId);
        public ProjectTasks FindProjectTaskByCondition(Expression<Func<ProjectTasks, bool>> expression);
        public TasksViewModel GetTaskViewModel(string userId);
        public List<ProjectTasks> FindAllByUserIdOrPM(string userId);
        public List<ProjectTasks> FindAll(Users user);
    }
}
