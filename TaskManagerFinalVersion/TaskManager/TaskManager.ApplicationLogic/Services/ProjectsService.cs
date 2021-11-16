using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.Dtos;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Repositories;
using TaskManager.DataAccess.Repositories.Abstractions;

namespace TaskManager.ApplicationLogic.Services
{
    public class ProjectsService: IProjectsService
    {
        public IProjectsRepository ProjectsRepository { get; }

        public ProjectsService(TaskManagerDbContext context)
        {
            ProjectsRepository = new ProjectsRepository(context);
        }

        public void AddProject(Projects project)
        {
            ProjectsRepository.Create(project);
            ProjectsRepository.Save();
        }

        public void UpdateProject(Projects project)
        {
            ProjectsRepository.Update(project);
            ProjectsRepository.Save();

        }

        public void DeleteProject(ProjectsViewModel model)
        {
            foreach (var project in model.Projects)
            {
                if (project.Selected)
                {
                    ProjectsRepository.Delete(project);
                }

            }
            ProjectsRepository.Save();
        }

        public List<Projects> FindAll()
        {
            return ProjectsRepository.FindAll();
        }

        public Projects FindByCondition(Expression<Func<Projects, bool>> expression)
        {
            return ProjectsRepository.FindByCondition(expression);
        }

        public List<Projects> FindProjectByUserId(string userId)
        {
            return ProjectsRepository.FindProjectByUserId(userId);
        }

        public List<Projects> FindProjectByPM(string userId)
        {
            return ProjectsRepository.FindByPM(userId);
        }

        public bool ProjectExists(int id)
        {
            return ProjectsRepository.FindAll().Any(e => e.ProjectsId == id);
        }

        
    }
}
