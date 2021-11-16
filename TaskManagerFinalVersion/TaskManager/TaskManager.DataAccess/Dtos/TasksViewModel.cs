using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.DataAccess.Dtos
{
    public class TasksViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public List<ProjectTasks> ProjectTasks { get; set; }
    }
}
