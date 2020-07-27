using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Services
{
    public class BadgesService : IBadgesService
    {
        public IBadgesRepository BadgesRepository { get; set; }

        public BadgesService(TaskManagerDbContext context)
        {
            BadgesRepository = new BadgesRepository(context);
        }

        public void AddBadge(Badges badge)
        {
            BadgesRepository.Create(badge);
            BadgesRepository.Save();
        }

        public void DeleteBadge(Badges badge)
        {
            var foundBadge = BadgesRepository.FindByCondition(b => b.BadgesId == badge.BadgesId);
            if(badge != null)
            {
                BadgesRepository.Delete(foundBadge);
                BadgesRepository.Save();
            }
           
        }

        public void UpdateBadge(Badges badge)
        {
            var foundBadge = BadgesRepository.FindByCondition(b => b.BadgesId == badge.BadgesId);
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
                BadgesRepository.Update(foundBadge);
                BadgesRepository.Save();
            }
            
        }

        public Badges FindBadgesByCondition(Expression<Func<Badges, bool>> expression)
        {
            return BadgesRepository.FindByCondition(expression);
        }

        public bool BadgeExists(int id)
        {
            return BadgesRepository.BadgeExists(id);
        }

        public List<Badges> FindAllBadges()
        {
            return BadgesRepository.FindAll();
        }
    }
}
