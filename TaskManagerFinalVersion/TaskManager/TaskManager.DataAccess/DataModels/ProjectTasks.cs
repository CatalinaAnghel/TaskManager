using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskManager.DataAccess.DataModels
{
    public class ProjectTasks
    {
        public int ProjectTasksId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Importance { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public int Points { get; set; }
        [NotMapped]
        public bool Selected { get; set; }
        [JsonIgnore]
        public Users User { get; set; }
        [JsonIgnore]
        public Projects Project { get; set; }

    }
}
