using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.DataAccess.Repositories.Abstractions
{
    public interface IUsersRepository: IRepositoryBase<Users>
    {
        public List<Users> FindColleagues(Users user);
    }
}
