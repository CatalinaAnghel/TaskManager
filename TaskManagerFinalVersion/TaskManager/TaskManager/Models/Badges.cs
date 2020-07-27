using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Badges
    {
        public int BadgesId { get; set; }
        public string Name { get; set; }
        public int NecessaryScore { get; set; }

        // Navigation Properties
        public ICollection<UserBadges> UserBadges { get; set; }
    }
}
