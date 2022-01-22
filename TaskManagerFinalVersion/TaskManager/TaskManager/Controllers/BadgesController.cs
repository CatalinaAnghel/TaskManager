using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BadgesController : Controller
    {
        private readonly IBadgesService _badgesService;

        public BadgesController(IBadgesService badgesService)
        {
            _badgesService = badgesService;
        }

        // GET: Badges
        public IActionResult Index()
        {
            return View(_badgesService.FindAllBadges().OrderBy(b => b.NecessaryScore));
        }

        // GET: Badges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Badges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BadgesId,Name,NecessaryScore")] Badges badges)
        {
            if (ModelState.IsValid)
            {
                _badgesService.AddBadge(badges);
                return RedirectToAction(nameof(Index));
            }
            return View(badges);
        }

        // GET: Badges/Edit/5
        public IActionResult Edit()
        {
            // select the badges that can be edited
            ViewData["BadgesId"] = new SelectList(_badgesService.FindAllBadges(), "BadgesId", "Name");

            return View();
        }

        // POST: Badges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("BadgesId,Name,NecessaryScore")] Badges badge)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _badgesService.UpdateBadge(badge);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_badgesService.BadgeExists(badge.BadgesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BadgesId"] = new SelectList(_badgesService.FindAllBadges(), "BadgesId", "Name");

            return View(badge);
        }

        // GET: Badges/Delete/5
        public ActionResult Delete()
        {
            ViewData["BadgesId"] = new SelectList(_badgesService.FindAllBadges(), "BadgesId", "Name");

            return View();
        }

        // POST: Badges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind("BadgesId")] Badges badge)
        {
            _badgesService.DeleteBadge(badge);
            return RedirectToAction(nameof(Index));
        }
        
        public ActionResult GetBadgeDetails(int id)
        {
            return Json(_badgesService.FindBadgesByCondition(b => b.BadgesId == id));
        }
    }
}
