﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface ITeamsRepository : IRepositoryBase<Teams>
    {
        public List<Teams> FindTeamsByPM(Users user);
    }
}
