using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.DataModels;
using TaskManager.DataAccess.UnitOfWork;

namespace TaskManager.ApplicationLogic.Services
{
    public class BadgesService : IBadgesService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public BadgesService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void AddBadge(Badges badge)
        {
            UnitOfWork.BadgesRepository.Create(badge);
            UnitOfWork.Complete();
        }

        public void DeleteBadge(Badges badge)
        {
            var foundBadge = UnitOfWork.BadgesRepository.FindByCondition(b => b.BadgesId == badge.BadgesId);
            if(badge != null)
            {
                UnitOfWork.BadgesRepository.Delete(foundBadge);
                UnitOfWork.Complete();
            }
           
        }

        public void UpdateBadge(Badges badge)
        {
            var foundBadge = UnitOfWork.BadgesRepository.FindByCondition(b => b.BadgesId == badge.BadgesId);
            if (foundBadge != null)
            {
                if(foundBadge.Name != badge.Name)
                {
                    foundBadge.Name = badge.Name;
                }
                if (foundBadge.NecessaryScore != badge.NecessaryScore)
                {
                    foundBadge.NecessaryScore = badge.NecessaryScore;
                }
                UnitOfWork.BadgesRepository.Update(foundBadge);
                UnitOfWork.Complete();
            }
            
        }

        public Badges FindBadgesByCondition(Expression<Func<Badges, bool>> expression)
        {
            return UnitOfWork.BadgesRepository.FindByCondition(expression);
        }

        public bool BadgeExists(int id)
        {
            return UnitOfWork.BadgesRepository.BadgeExists(id);
        }

        public List<Badges> FindAllBadges()
        {
            return UnitOfWork.BadgesRepository.FindAll();
        }
    }
}
