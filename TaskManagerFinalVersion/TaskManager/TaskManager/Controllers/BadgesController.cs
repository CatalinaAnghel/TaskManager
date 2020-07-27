using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Interfaces.Services;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BadgesController : Controller
    {
        private readonly IBadgesService _badgesService;

        public BadgesController(TaskManagerDbContext context, IBadgesService badgesService)
        {
            _badgesService = badgesService;
        }

        // GET: Badges
        public async Task<IActionResult> Index()
        {
            return View(_badgesService.FindAllBadges().OrderBy(b => b.NecessaryScore));
        }

        //// GET: Badges/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //var badges = await _context.Badges
        //    //    .FirstOrDefaultAsync(m => m.BadgesId == id);
        //    var badges = _badgesService.FindBadgesByCondition(b => b.BadgesId == id);
        //    if (badges == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(badges);
        //}

        // GET: Badges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Badges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["BadgesId"] = new SelectList(_badgesService.FindAllBadges(), "BadgesId", "Name");

            return View();
        }

        // POST: Badges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("BadgesId,Name,NecessaryScore")] Badges badge)
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
