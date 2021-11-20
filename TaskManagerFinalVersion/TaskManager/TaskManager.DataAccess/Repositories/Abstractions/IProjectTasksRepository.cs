using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.DataAccess.Repositories.Abstractions
{
    public interface IProjectTasksRepository : IRepositoryBase<ProjectTasks>
    {
        public List<ProjectTasks> SeeTasks(string userId);

        public int GetNumberOfUrgentTasks(string userId);
        public int GetNumberOfFinishedTasks(string userId);
        public int GetNumberOfUnfinishedTasks(string userId);
        public ProjectTasks FindProjectTaskByCondition(Expression<Func<ProjectTasks, bool>> expression);
        public List<ProjectTasks> FindAllByUserIdOrPM(string userId);
        public List<ProjectTasks> FindAll(Users user);
    }
}
