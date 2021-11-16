using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.DataAccess.Repositories.Abstractions
{
    public interface IProjectsRepository : IRepositoryBase<Projects>
    {
        public void AddProject(Projects project);
        public void DeleteProject(Projects project);
        public void UpdateProject(Projects project);
        new Projects FindByCondition(Expression<Func<Projects, bool>> expression);
        public List<Projects> FindProjectByUserId(string userId);
        public List<Projects> FindByPM(string userId);
    }
}
