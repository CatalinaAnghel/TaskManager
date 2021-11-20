using System.Collections.Generic;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.ApplicationLogic.Dtos
{
    public class ProjectsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public List<Projects> Projects { get; set; }
    }
}
