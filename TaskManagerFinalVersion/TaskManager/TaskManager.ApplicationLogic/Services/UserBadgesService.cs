using System.Collections.Generic;
using TaskManager.DataAccess.DataModels;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.UnitOfWork;

namespace TaskManager.ApplicationLogic.Services
{
    public class UserBadgesService : IUserBadgesService
    {
        public IUnitOfWork UnitOfWork { get; }
        public UserBadgesService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public List<UserBadges> FindBadges(Users user)
        {
            return UnitOfWork.UserBadgesRepository.FindBadges(user);
        }
    }
}
