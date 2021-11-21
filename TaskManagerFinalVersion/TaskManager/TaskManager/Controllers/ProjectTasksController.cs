using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.ApplicationLogic.Dtos;
using TaskManager.ApplicationLogic.Helpers;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.DataModels;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize]
    public class ProjectTasksController : BaseController
    {
        private readonly IProjectTasksService _taskService;
        private readonly IProjectsService _projectsService;
        private readonly IUsersService _usersService;

        public ProjectTasksController(IProjectTasksService service,
                                      IProjectsService projectsService,
                                      IUsersService usersService)
        {
            _taskService = service;
            _projectsService = projectsService;
            _usersService = usersService;
        }


        // GET: ProjectTasks
        public async Task<IActionResult> Index()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            return View(_taskService.SeeTasks(user.Id));
        }

        // GET: ProjectTasks
        public async Task<IActionResult> SeeAllTasks()
        {
            // the tasks can be seen if the tasks belong to one of the user's team
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            return View(_taskService.FindAll(user));
        }

        [HttpGet]
        public async Task<IActionResult> Assign()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            ViewData["ProjectsId"] = new SelectList(_projectsService.FindProjectByPM(user.Id), "ProjectsId", "Name");
            ViewData["UsersId"] = new SelectList(_usersService.FindColleagues(user), "Id", "UserName");
            return View();
        }

        [HttpPost, ActionName("Assign")]
        public async Task<IActionResult> Assign([Bind("ProjectId,Name,UserId")] ProjectTasks projectTasks)
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            if (ModelState.IsValid)
            {
                _taskService.AssignTaskToUser(projectTasks.ProjectId, projectTasks.Name, projectTasks.UserId);
                return RedirectToAction("Index", "Projects");
            }
            ViewData["ProjectsId"] = new SelectList(_projectsService.FindProjectByPM(user.Id), "ProjectsId", "Name");
            ViewData["UsersId"] = new SelectList(_usersService.FindColleagues(user), "Id", "UserName");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProjectTasks()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            return View(_taskService.GetTaskViewModel(user.Id));
        }

        [HttpPost, ActionName("DeleteProjectTasks")]
        public IActionResult DeleteProjectTasks(TasksDto model)
        {
            if (ModelState.IsValid)
            {
                _taskService.DeleteTask(model);

            }
            return RedirectToAction("Index", "Projects");
        }


        [HttpGet]
        public IActionResult GetTasks(int id)
        {
            return Json(_taskService.FindAllByProject(id));
        }

        // GET: ProjectTasks/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTasks = _taskService.FindByCondition(t => t.ProjectTasksId == id);
            if (projectTasks == null)
            {
                return NotFound();
            }

            return View(projectTasks);
        }

        // GET: ProjectTasks/Create
        public async Task<IActionResult> Create()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);

            ViewData["ProjectId"] = new SelectList(_projectsService.FindProjectByPM(user.Id), "ProjectsId", "Name");
            ViewData["UserId"] = new SelectList(_usersService.FindAll(), "Id", "UserName");
            ViewData["ImportanceLevelDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(ImportanceLevel))
                );

            ViewData["StatusDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(ApplicationLogic.Helpers.TaskStatus))
                );

            return View();
        }

        // POST: ProjectTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectTasksId,Name,Description,Importance,DueDate,Status,ProjectId,Points")] ProjectTasks projectTasks)
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);

            if (ModelState.IsValid)
            {
                _taskService.AddTask(projectTasks);

                return RedirectToAction(nameof(Index));
            }

            ViewData["ProjectId"] = new SelectList(_projectsService.FindProjectByPM(user.Id), "ProjectsId", "Name");
            ViewData["UserId"] = new SelectList(_usersService.FindAll(), "Id", "UserName");
            ViewData["ImportanceLevelDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(ImportanceLevel))
                );

            ViewData["StatusDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(ApplicationLogic.Helpers.TaskStatus))
                );
            return View(projectTasks);
        }

        //[HttpGet]
        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);

            var projectTasks = _taskService.FindAllByUserIdOrPM(user.Id);
            var projects = _projectsService.FindProjectByPM(user.Id);

            ViewData["ProjectTasksId"] = new SelectList(projectTasks, "ProjectTasksId", "Name");
            ViewData["ProjectId"] = new SelectList(projects, "ProjectsId", "Name");
            ViewData["UserId"] = new SelectList(_usersService.FindAll(), "Id", "UserName");
            ViewData["ImportanceLevelDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(ImportanceLevel))
                );

            ViewData["StatusDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(ApplicationLogic.Helpers.TaskStatus))
                );

            return View();
        }

        // POST: ProjectTasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ProjectTasksId,Name,Description,Importance,DueDate,Status,UserId,ProjectId,Points")] ProjectTasks projectTasks)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _taskService.UpdateTask(projectTasks);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_taskService.ProjectTasksExists(projectTasks.ProjectTasksId))
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
            ViewData["ProjectId"] = new SelectList(_projectsService.FindProjectByPM(projectTasks.UserId), "ProjectsId", "Name");
            ViewData["UserId"] = new SelectList(_usersService.FindAll(), "Id", "UserName");
            ViewData["ImportanceLevelDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(ImportanceLevel))
                );

            ViewData["StatusDropdown"] = this.BuildDropdownViewModel(
                Enum.GetValues(typeof(ApplicationLogic.Helpers.TaskStatus))
                );
            return View(projectTasks);
        }

        public IActionResult GetTaskDetails(int? id)
        {
            var task = _taskService.FindByCondition(x => x.ProjectTasksId == id);
            Console.WriteLine("\n" + task + " \n");
            return Json(task);
        }
    }
}
