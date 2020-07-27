using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IUsersRepository: IRepositoryBase<Users>
    {
        public List<Users> FindColleagues(Users user);
    }
}
