using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Repositories.Abstractions;
using TaskManager.DataAccess.Helpers;

namespace TaskManager.DataAccess.Repositories
{
    public class ProjectTasksRepository : RepositoryBase<ProjectTasks>, IProjectTasksRepository
    {
        public ProjectTasksRepository(TaskManagerDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public ProjectTasks FindProjectTaskByCondition(Expression<Func<ProjectTasks, bool>> expression)
        {
            return RepositoryContext.ProjectTasks
                .Where(expression)
                .Include(t=>t.Project)
                .Include(t=>t.User)
                .SingleOrDefault();
        }

        public List<ProjectTasks> SeeTasks(string userId)
        {
            return RepositoryContext.ProjectTasks
                .Where(t => t.UserId == userId)
                .Include(t => t.Project)
                .Include(t => t.User)
                .ToList();
        }

        public int GetNumberOfUrgentTasks(string userId)
        {
            var urgentTasksNumber = (from task in RepositoryContext.ProjectTasks
                                     where (task.UserId.Equals(userId) &&
                                     EF.Functions.DateDiffDay(DateTime.Now, task.DueDate) < 7 &&
                                     task.Status.Equals(TaskStatus.Done.ToString()))
                                     select task).Count();

            return urgentTasksNumber;
        }

        public int GetNumberOfFinishedTasks(string userId)
        {
            var urgentTasksNumber = (from task in RepositoryContext.ProjectTasks
                                     where (task.UserId.Equals(userId) &&
                                     task.Status.Equals(TaskStatus.Done.ToString()))
                                     select task).Count();
            return urgentTasksNumber;
        }

        public int GetNumberOfUnfinishedTasks(string userId)
        {
            var urgentTasksNumber = (from task in RepositoryContext.ProjectTasks
                                     where (task.UserId.Equals(userId) &&
                                     !task.Status.Equals(TaskStatus.Done.ToString()))
                                     select task).Count();
            return urgentTasksNumber;
        }

        public List<ProjectTasks> FindAllByUserIdOrPM(string userId)
        {
            var tasks = ((from task in RepositoryContext.ProjectTasks
                         where task.UserId.Equals(userId)
                         select task)
                         .Concat(from task in RepositoryContext.ProjectTasks
                                 join t in RepositoryContext.Teams on task.ProjectId equals t.ProjectId
                                 join ut in RepositoryContext.UserTeams on t.TeamsId equals ut.TeamsId
                                 where ut.Job.Equals("PM") && ut.UsersId.Equals(userId)
                                 select task))
                                .Distinct()
                                .ToList();
            return tasks;
        }

        public List<ProjectTasks> FindAll(Users user)
        {
            var projects = (from p in RepositoryContext.Projects
                            join t in RepositoryContext.Teams on p.ProjectsId equals t.ProjectId
                            join ut in RepositoryContext.UserTeams on t.TeamsId equals ut.TeamsId
                            where ut.UsersId == user.Id
                            select p.ProjectsId).Distinct().ToList();
            return RepositoryContext.ProjectTasks
                .Where(t=> projects.Contains(t.ProjectId))
                .Include(t => t.Project)
                .Include(t => t.User).ToList();
        }
    }
}
