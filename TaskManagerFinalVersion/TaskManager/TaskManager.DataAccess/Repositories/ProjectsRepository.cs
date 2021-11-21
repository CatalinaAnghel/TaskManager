using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Repositories.Abstractions;

namespace TaskManager.DataAccess.Repositories
{
    public class ProjectsRepository: RepositoryBase<Projects>, IProjectsRepository
    {
        public ProjectsRepository(TaskManagerDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void AddProject(Projects project)
        {
            Create(project);
        }

        public void DeleteProject(Projects project)
        {
            Delete(project);
        }

        public void UpdateProject(Projects project)
        {
            Update(project);
        }

        public Projects FindProjectByCondition(Expression<Func<Projects, bool>> expression)
        {
            return FindByCondition(expression);
        }

        public List<Projects> FindProjectByUserId(string userId)
        {
            var projects = (from project in RepositoryContext.Projects
                            join t in RepositoryContext.Teams on project.ProjectsId equals t.ProjectId
                            join ut in RepositoryContext.UserTeams on t.TeamsId equals ut.TeamsId
                            where ut.UsersId == userId
                            select project).Distinct().ToList();
            return projects;
        }

        public List<Projects> FindByPM(string userId)
        {
            var projects = (from project in RepositoryContext.Projects
                            join t in RepositoryContext.Teams on project.ProjectsId equals t.ProjectId
                            join ut in RepositoryContext.UserTeams on t.TeamsId equals ut.TeamsId
                            where ut.UsersId == userId && ut.Job.Equals("PM")
                            select project).Distinct().ToList();
            return projects;
        }

    }
}
