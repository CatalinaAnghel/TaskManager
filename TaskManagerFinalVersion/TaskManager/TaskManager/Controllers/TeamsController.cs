using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Interfaces.Services;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IProjectsService _projectsService;
        private readonly ITeamsService _teamsService;
        public TeamsController( IProjectsService projectsService,
                                ITeamsService teamsService,
                                IUsersService usersService)
        {
            _usersService = usersService;
            _projectsService = projectsService;
            _teamsService = teamsService;
        }

        // GET: Teams
        public IActionResult Index()
        {
            return View(_teamsService.FindAll());
        }

        // GET: Teams/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = _teamsService.FindByCondition(t => t.TeamsId == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {

            ViewData["ProjectId"] = new SelectList(_projectsService.FindAll(), "ProjectsId", "Name");
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamsId,Name,ProjectId")] Teams teams)
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            if (ModelState.IsValid)
            {
                _teamsService.AddTeam(user, teams);
                return RedirectToAction("Index", "Projects");
            }
            ViewData["ProjectId"] = new SelectList(_projectsService.FindAll(), "ProjectsId", "Name");
            return View(teams);
        }

        // GET: Teams/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = _teamsService.FindByCondition(t => t.ProjectId == id);
            if (teams == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_projectsService.FindAll(), "ProjectsId", "Name");
            return View(teams);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("TeamsId,Name,ProjectId")] Teams teams)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _teamsService.UpdateTeam(teams);
                }
                catch (DbUpdateConcurrencyException e){
                    throw new Exception("Concurrency error:", e);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_projectsService.FindAll(), "ProjectsId", "Name");
            return View(teams);
        }

        [HttpGet]
        public async Task<ActionResult> Delete()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            ViewData["Teams"] = new SelectList(_teamsService.FindTeamsByPM(user), "TeamsId", "Name");
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete([Bind("TeamsId")] Teams team)
        {
            

            var teams = _teamsService.FindByCondition(t => t.TeamsId == team.TeamsId);
            if (teams == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _teamsService.DeleteTeam(teams);
            }
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            ViewData["Teams"] = new SelectList(_teamsService.FindTeamsByPM(user), "TeamsId", "Name");

            return View(teams);
        }
    }
}
