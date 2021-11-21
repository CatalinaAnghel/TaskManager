using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskManager.DataAccess.DataModels;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.UnitOfWork;
using TaskManager.ApplicationLogic.Dtos;

namespace TaskManager.ApplicationLogic.Services
{
    public class ProjectsService: IProjectsService
    {
        public IUnitOfWork UnitOfWork { get; }

        public ProjectsService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void AddProject(Projects project)
        {
            UnitOfWork.ProjectsRepository.Create(project);
            UnitOfWork.Complete();
        }

        public void UpdateProject(Projects project)
        {
            UnitOfWork.ProjectsRepository.Update(project);
            UnitOfWork.Complete();

        }

        public void DeleteProject(ProjectsDto model)
        {
            foreach (var project in model.Projects)
            {
                if (project.Selected)
                {
                    UnitOfWork.ProjectsRepository.Delete(project);
                }

            }
            UnitOfWork.Complete();
        }

        public List<Projects> FindAll()
        {
            return UnitOfWork.ProjectsRepository.FindAll();
        }

        public Projects FindByCondition(Expression<Func<Projects, bool>> expression)
        {
            return UnitOfWork.ProjectsRepository.FindByCondition(expression);
        }

        public List<Projects> FindProjectByUserId(string userId)
        {
            return UnitOfWork.ProjectsRepository.FindProjectByUserId(userId);
        }

        public List<Projects> FindProjectByPM(string userId)
        {
            return UnitOfWork.ProjectsRepository.FindByPM(userId);
        }

        public bool ProjectExists(int id)
        {
            return UnitOfWork.ProjectsRepository.FindAll().Any(e => e.ProjectsId == id);
        }

        
    }
}
