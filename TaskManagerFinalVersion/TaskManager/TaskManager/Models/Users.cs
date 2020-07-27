using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Users: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Score { get; set; }
        public byte[] ProfileImage { get; set; }

        public ICollection<ProjectTasks> ProjectTasks { get; set; }
        public ICollection<UserTeams> UserTeams { get; set; }
        public ICollection<UserBadges> UserBadges { get; set; }
    }
}
