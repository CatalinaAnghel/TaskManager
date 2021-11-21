using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManager.ApplicationLogic.Dtos;
using TaskManager.ApplicationLogic.Helpers;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.Controllers
{
    [Authorize(Roles="Administrator, User")]
    public class ProjectsController : BaseController
    {
        private readonly IProjectsService _projectsService;
        private readonly IProjectTasksService _tasksService;
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
            int number = HttpContext.Session.GetString("DashboardVisits") != null? 
                Int32.Parse(HttpContext.Session.GetString("DashboardVisits")) + 1 : 1;
            
            ViewData["NoVisits"] = number ;

            ViewData["UrgentTask"] = _tasksService.GetNumberOfUrgentTasks(user.Id).ToString();
            ViewData["UnfinishedTasks"] = _tasksService.GetNumberOfUnfinishedTasks(user.Id).ToString();
            ViewData["FinishedTasks"] = _tasksService.GetNumberOfFinishedTasks(user.Id).ToString();
            
            HttpContext.Session.SetString("DashboardVisits", JsonConvert.SerializeObject(number));
            return View(_projectsService.FindProjectByUserId(user.Id));
        }

        // GET: Projects/Details/5
        public IActionResult Details(int? id)
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
            ViewData["ImportanceLevelDropdown"] = this.BuildDropdownViewModel(
               Enum.GetValues(typeof(ImportanceLevel))
               );

            ViewData["DifficultyLevelDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(DifficultyLevel))
                );
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProjectsId,Name,StartDate,EndDate,Description,WorkedHours,Difficulty,Link,Importance")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                _projectsService.AddProject(projects);

                return RedirectToAction("Create", "Teams");
            }

            ViewData["ImportanceLevelDropdown"] = this.BuildDropdownViewModel(
               Enum.GetValues(typeof(ImportanceLevel))
               );

            ViewData["DifficultyLevelDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(DifficultyLevel))
                );
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
            ViewData["ImportanceLevelDropdown"] = this.BuildDropdownViewModel(
               Enum.GetValues(typeof(ImportanceLevel))
               );

            ViewData["DifficultyLevelDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(DifficultyLevel))
                );
            return View();
        }

        // POST: Projects/Edit/5
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
            ViewData["ImportanceLevelDropdown"] = this.BuildDropdownViewModel(
               Enum.GetValues(typeof(ImportanceLevel))
               );

            ViewData["DifficultyLevelDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(DifficultyLevel))
                );
            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProject()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            ProjectsDto model = new ProjectsDto
            {
                Projects = _projectsService.FindProjectByPM(user.Id)
            };
            return View(model);
        }

        [HttpPost, ActionName("DeleteProjects")]
        public ActionResult DeleteProject(ProjectsDto model)
        {
            if (ModelState.IsValid)
            {
                _projectsService.DeleteProject(model);
            }
            return RedirectToAction("Index", "Projects");
        }
    }
}
