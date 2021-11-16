using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.DataAccess.DataModels
{
    public class UserBadges
    {
        public int UserBadgesId { get; set; }
        public string UsersId { get; set; }
        public int BadgeId { get; set; }

        public Users User { get; set; }
        public Badges Badge { get; set; }
    }
}
