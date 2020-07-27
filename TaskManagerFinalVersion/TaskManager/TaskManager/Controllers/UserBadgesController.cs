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
    [Authorize]
    public class UserBadgesController : Controller
    {
        private readonly IUserBadgesService _userBadgesService;
        private readonly IUsersService _usersService;

        public UserBadgesController(IUserBadgesService userBadgesService,
                                    IUsersService usersService)
        {
            _userBadgesService = userBadgesService;
            _usersService = usersService;
        }

        // GET: /UserBadges/SeeBadges
        public async Task<ActionResult> SeeBadges()
        {
            var currentUser = await _usersService.GetCurrentUser(HttpContext.User);
            ViewData["Score"] = currentUser.Score;
            return View(_userBadgesService.FindBadges(currentUser));
        }

        //// GET: UserBadges
        //public async Task<IActionResult> Index()
        //{
        //    var taskManagerDbContext = _context.UserBadges.Include(u => u.Badge).Include(u => u.User);
        //    return View(await taskManagerDbContext.ToListAsync());
        //}

        //// GET: UserBadges/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userBadges = await _context.UserBadges
        //        .Include(u => u.Badge)
        //        .Include(u => u.User)
        //        .FirstOrDefaultAsync(m => m.UserBadgesId == id);
        //    if (userBadges == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userBadges);
        //}

        //// GET: UserBadges/Create
        //public IActionResult Create()
        //{
        //    ViewData["BadgeId"] = new SelectList(_context.Badges, "BadgesId", "BadgesId");
        //    ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id");
        //    return View();
        //}

        //// POST: UserBadges/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("UserBadgesId,UsersId,BadgeId")] UserBadges userBadges)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(userBadges);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BadgeId"] = new SelectList(_context.Badges, "BadgesId", "BadgesId", userBadges.BadgeId);
        //    ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id", userBadges.UsersId);
        //    return View(userBadges);
        //}

        //// GET: UserBadges/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userBadges = await _context.UserBadges.FindAsync(id);
        //    if (userBadges == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["BadgeId"] = new SelectList(_context.Badges, "BadgesId", "BadgesId", userBadges.BadgeId);
        //    ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id", userBadges.UsersId);
        //    return View(userBadges);
        //}

        //// POST: UserBadges/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("UserBadgesId,UsersId,BadgeId")] UserBadges userBadges)
        //{
        //    if (id != userBadges.UserBadgesId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(userBadges);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserBadgesExists(userBadges.UserBadgesId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BadgeId"] = new SelectList(_context.Badges, "BadgesId", "BadgesId", userBadges.BadgeId);
        //    ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id", userBadges.UsersId);
        //    return View(userBadges);
        //}

        //// GET: UserBadges/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userBadges = await _context.UserBadges
        //        .Include(u => u.Badge)
        //        .Include(u => u.User)
        //        .FirstOrDefaultAsync(m => m.UserBadgesId == id);
        //    if (userBadges == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userBadges);
        //}

        //// POST: UserBadges/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var userBadges = await _context.UserBadges.FindAsync(id);
        //    _context.UserBadges.Remove(userBadges);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserBadgesExists(int id)
        //{
        //    return _context.UserBadges.Any(e => e.UserBadgesId == id);
        //}
    }
}
