﻿using System;
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
    public class UserTeamsController : Controller
    {
        private readonly IUserTeamsService _userTeamsService;
        private readonly IUsersService _usersService;
        private readonly ITeamsService _teamsService;

        public UserTeamsController(IUserTeamsService userTeamsService,
                                    IUsersService usersService, 
                                    ITeamsService teamsService)
        {
            _userTeamsService = userTeamsService;
            _usersService = usersService;
            _teamsService = teamsService;
        }

        // GET: UserTeams/Create
        public async Task<IActionResult> Create()
        {
            var currentUser = await _usersService.GetCurrentUser(HttpContext.User);
            ViewData["TeamsId"] = new SelectList(_teamsService.FindTeamsByPM(currentUser), "TeamsId", "Name");
            ViewData["UsersId"] = new SelectList(_usersService.FindAll(), "Id", "UserName");
            return View();
        }

        // POST: UserTeams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserTeamsId,UsersId,TeamsId,Job")] UserTeams userTeams)
        {
            if (ModelState.IsValid)
            {
                _userTeamsService.AddUserInTeam(userTeams);
                return RedirectToAction("Index", "Projects");
            }
            var currentUser = await _usersService.GetCurrentUser(HttpContext.User);
            ViewData["TeamsId"] = new SelectList(_teamsService.FindTeamsByPM(currentUser), "TeamsId", "Name", userTeams.TeamsId);
            ViewData["UsersId"] = new SelectList(_usersService.FindAll(), "Id", "UserName", userTeams.UsersId);
            return View(userTeams);
        }

     

        // GET: UserTeams/Delete/5
        public async Task<IActionResult> Delete()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            var teams = _teamsService.FindTeamsByPM(user);
            
            ViewData["Teams"] = new SelectList(teams, "TeamsId", "Name");
            return View();
        }

        public async Task<IActionResult> FindColleagues(int id)
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);

            var users = _userTeamsService.GetTeamColleagues(id, user);
            return Json(users);
        }

        // POST: UserTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete([Bind("UsersId, TeamsId")] UserTeams userTeam)
        {
            if (ModelState.IsValid)
            {
                _userTeamsService.DeleteUserFromTeam(userTeam);
                return RedirectToAction("Index", "Projects");
            }

            var user = await _usersService.GetCurrentUser(HttpContext.User);
            var teams = _teamsService.FindTeamsByPM(user);
            ViewData["Teams"] = new SelectList(teams, "TeamsId", "Name");
            return View();
        }

        //private bool UserTeamsExists(int id)
        //{
        //    return _context.UserTeams.Any(e => e.UserTeamsId == id);
        //}
    }
}
