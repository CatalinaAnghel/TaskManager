using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.DataAccess.DataModels
{
    public class UserTeams
    {
        public int UserTeamsId { get; set; }
        public string UsersId { get; set; }
        public int TeamsId { get; set; }
        public string Job { get; set; }

        public Users User { get; set; }
        public Teams Team { get; set; }
    }
}
