using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Teams
    {
        public int TeamsId { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }

        public Projects Project { get; set; }

        public ICollection<UserTeams> UserTeams { get; set; }
    }
}
