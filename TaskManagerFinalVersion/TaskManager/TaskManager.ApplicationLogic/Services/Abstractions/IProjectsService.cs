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
    public interface IProjectsService
    {
        IProjectsRepository ProjectsRepository { get; }
      
        public void AddProject(Projects project);
        public void UpdateProject(Projects project);
        public void DeleteProject(ProjectsViewModel model);
        public List<Projects> FindAll();
        public List<Projects> FindProjectByUserId(string userId);
        public List<Projects> FindProjectByPM(string userId);
        public bool ProjectExists(int id);
        public Projects FindByCondition(Expression<Func<Projects, bool>> expression);
    }
}
