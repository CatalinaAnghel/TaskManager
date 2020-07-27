using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManager.Data;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    [Authorize(Roles="Administrator, User")]
    public class ProjectsController : Controller
    {
        private IProjectsService _projectsService;
        private IProjectTasksService _tasksService;
        private readonly IUsersService _usersService;

        public ProjectsController(IProjectsService projectsService,
                                   IProjectTasksService tasksService,
                                   IUsersService usersService)
        {
            _projectsService = projectsService;
            _tasksService = tasksService;
            _usersService = usersService;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            var number = HttpContext.Session.GetString("DashboardVisits") + 1;
            
            ViewData["NoVisits"] = number ;

            ViewData["UrgentTask"] = _tasksService.GetNumberOfUrgentTasks(user.Id).ToString();
            ViewData["UnfinishedTasks"] = _tasksService.GetNumberOfUnfinishedTasks(user.Id).ToString();
            ViewData["FinishedTasks"] = _tasksService.GetNumberOfFinishedTasks(user.Id).ToString();
            
            HttpContext.Session.SetString("DashboardVisits", JsonConvert.SerializeObject(number));
            return View(_projectsService.FindProjectByUserId(user.Id));
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = _projectsService.FindByCondition(project => project.ProjectsId == id);
            if (projects == null)
            {
                return NotFound();
            }

            return View(projects);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectsId,Name,StartDate,EndDate,Description,WorkedHours,Difficulty,Link,Importance")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                _projectsService.AddProject(projects);

                return RedirectToAction("Create", "Teams");
            }
            return View(projects);
        }

        public IActionResult GetProjectDetails(int? id)
        {
            // function called using AJAX when the user wants to edit a project
            return Json(_projectsService.FindByCondition(project => project.ProjectsId == id));
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            ViewData["ProjectsId"] = new SelectList(_projectsService.FindProjectByUserId(user.Id), "ProjectsId", "Name");

            return View();
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ProjectsId,Name,StartDate,EndDate,Description,WorkedHours,Difficulty,Link,Importance")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _projectsService.UpdateProject(projects);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_projectsService.ProjectExists(projects.ProjectsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            ViewData["ProjectsId"] = new SelectList(_projectsService.FindProjectByUserId(user.Id), "ProjectsId", "Name");

            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProject()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            ProjectsViewModel model = new ProjectsViewModel
            {
                Projects = _projectsService.FindProjectByPM(user.Id)
            };
            return View(model);
        }

        [HttpPost, ActionName("DeleteProjects")]
        public ActionResult DeleteProject(ProjectsViewModel model)
        {
            if (ModelState.IsValid)
            {
                _projectsService.DeleteProject(model);
            }
            return RedirectToAction("Index", "Projects");
        }


        //// GET: Projects/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //var projects = _repoWrapper.Projects.FindByCondition(p => p.ProjectsId == id);
        //    var projects = _projectsService.FindByCondition(project => project.ProjectsId == id);
        //    if (projects == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(projects);
        //}

        //// POST: Projects/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    //var projects = _repoWrapper.Projects.FindByCondition(p => p.ProjectsId == id);
        //    //_repoWrapper.Projects.Delete(projects);
        //    //_repoWrapper.Save();
        //    //_projectsService.DeleteProject(_projectsService.FindByCondition(p => p.ProjectsId == id));

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
