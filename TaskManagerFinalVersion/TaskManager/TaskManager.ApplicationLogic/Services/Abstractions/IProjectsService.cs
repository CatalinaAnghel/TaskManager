using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskManager.ApplicationLogic.Dtos;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface IProjectsService
    {     
        public void AddProject(Projects project);
        public void UpdateProject(Projects project);
        public void DeleteProject(ProjectsDto model);
        public List<Projects> FindAll();
        public List<Projects> FindProjectByUserId(string userId);
        public List<Projects> FindProjectByPM(string userId);
        public bool ProjectExists(int id);
        public Projects FindByCondition(Expression<Func<Projects, bool>> expression);
    }
}
