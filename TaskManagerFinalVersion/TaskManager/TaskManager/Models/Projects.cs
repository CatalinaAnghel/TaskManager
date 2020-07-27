using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Projects
    {
        public int ProjectsId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public float WorkedHours { get; set; }
        public string Difficulty { get; set; }
        public string Link { get; set; }
        public string Importance { get; set; }
        [NotMapped]
        public bool Selected { get; set; }

        public ICollection<ProjectTasks> ProjectTasks { get; set; }
        public ICollection<Teams> Teams { get; set; }
    }
}
