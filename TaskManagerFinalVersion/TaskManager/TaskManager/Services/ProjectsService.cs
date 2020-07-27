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
using TaskManager.ViewModels;

namespace TaskManager.Services
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
                if (project.Selected == true)
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
