using Microsoft.EntityFrameworkCore;
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
using TaskManager.ViewModels;

namespace TaskManager.Services
{
    public class ProjectTasksService : IProjectTasksService
    {
        public ProjectTasksService(TaskManagerDbContext context)
        {
            ProjectTasksRepository = new ProjectTasksRepository(context);
            TeamsRepository = new TeamsRepository(context);
            UserTeamsRepository = new UserTeamsRepository(context);
            UsersRepository = new UsersRepository(context);
            BadgesRepository = new BadgesRepository(context);
            UserBadgesRepository = new UserBadgesRepository(context);
        }

        public IProjectTasksRepository ProjectTasksRepository { get; }
        public ITeamsRepository TeamsRepository { get; }
        public IUserTeamsRepository UserTeamsRepository { get; }
        public IUsersRepository UsersRepository { get; }
        public IBadgesRepository BadgesRepository { get; }
        public IUserBadgesRepository UserBadgesRepository { get; }

        public void AddTask(ProjectTasks task)
        {
            task.User = null;
            task.UserId = null;
            ProjectTasksRepository.Create(task);
            ProjectTasksRepository.Save();
        }

        public void UpdateTask(ProjectTasks task)
        {
            var foundTask = ProjectTasksRepository.FindByCondition(t => t.ProjectTasksId == task.ProjectTasksId);
            if (task.Status.Equals("Done") && !foundTask.Status.Equals("Done"))
            {
                var foundUser = UsersRepository.FindByCondition(u => u.Id == task.UserId);
                if (foundUser != null)
                {
                    foundUser.Score += task.Points;
                    UsersRepository.Update(foundUser);
                    UsersRepository.Save();
                    var badge = BadgesRepository.GetBadge(foundUser);
                    if (badge != null)
                    {
                        UserBadges userBadge = new UserBadges
                        {
                            UsersId = foundUser.Id,
                            BadgeId = badge.BadgesId
                        };
                        UserBadgesRepository.Create(userBadge);
                    }

                }
            }
            else
            {
                if (foundTask.Status != task.Status)
                {
                    if (foundTask.Status.Equals("Done"))
                    {
                        var user = UsersRepository.FindByCondition(u => u.Id == foundTask.UserId);
                        if (user != null)
                        {
                            user.Score -= foundTask.Points;
                        }
                        UsersRepository.Update(user);
                        UsersRepository.Save();
                    }
                    foundTask.Status = task.Status;
                }
            }

            if (foundTask.Description != task.Description)
            {
                foundTask.Description = task.Description;
            }
            if (foundTask.DueDate != task.DueDate)
            {
                foundTask.DueDate = task.DueDate;
            }
            if (foundTask.Importance != task.Importance)
            {
                foundTask.Importance = task.Importance;
            }
            if (foundTask.Name != task.Name)
            {
                foundTask.Name = task.Name;
            }
            if (foundTask.Points != task.Points)
            {
                foundTask.Points = task.Points;
            }
            if (foundTask.UserId != task.UserId)
            {
                foundTask.UserId = task.UserId;
            }

            foundTask.Status = task.Status;
            ProjectTasksRepository.Update(foundTask);
            ProjectTasksRepository.Save();

        }

        public void DeleteTask(ProjectTasks task)
        {
            ProjectTasksRepository.Delete(task);
            ProjectTasksRepository.Save();
        }

        public int GetNumberOfUrgentTasks(string userId)
        {
            return ProjectTasksRepository.GetNumberOfUrgentTasks(userId);
        }

        public int GetNumberOfFinishedTasks(string userId)
        {
            return ProjectTasksRepository.GetNumberOfFinishedTasks(userId);
        }

        public int GetNumberOfUnfinishedTasks(string userId)
        {
            return ProjectTasksRepository.GetNumberOfUnfinishedTasks(userId);
        }

        public List<ProjectTasks> SeeTasks(string userId)
        {
            return ProjectTasksRepository.SeeTasks(userId);
        }

        public bool ProjectTasksExists(int id)
        {
            return ProjectTasksRepository.FindAll().Any(e => e.ProjectId == id); ;
        }

        public List<ProjectTasks> FindAllByProject(int id)
        {
            return ProjectTasksRepository.FindAll().Where(e => e.ProjectId == id).ToList();

        }

        public ProjectTasks FindByCondition(Expression<Func<ProjectTasks, bool>> expression)
        {
            return ProjectTasksRepository.FindProjectTaskByCondition(expression);

        }

        public List<ProjectTasks> FindAllByUserIdOrPM(string userid)
        {

            return ProjectTasksRepository.FindAllByUserIdOrPM(userid);
        }

        public void AssignTaskToUser(int projectId, string taskName, string userId)
        {
            var task = ProjectTasksRepository.FindAll().Where(t => t.ProjectId == projectId).Where(t => t.Name == taskName).SingleOrDefault();
            task.UserId = userId;
            UpdateTask(task);

        }

        public TasksViewModel GetTaskViewModel(string userId)
        {
            return ProjectTasksRepository.GetTaskViewModel(userId);
        }

        public void DeleteTask(TasksViewModel model)
        {
            foreach (var task in model.ProjectTasks)
            {
                if (task.Selected == true)
                {
                    var foundTask = ProjectTasksRepository.FindByCondition(t => t.ProjectTasksId == task.ProjectTasksId);
                    if (foundTask.UserId != null && foundTask.Status.Equals("Done"))
                    {
                        var user = UsersRepository.FindByCondition(u => u.Id == foundTask.UserId);
                        if (user != null)
                        {
                            user.Score -= foundTask.Points;
                        }
                        UsersRepository.Update(user);
                        UsersRepository.Save();
                    }
                    ProjectTasksRepository.Delete(foundTask);
                    ProjectTasksRepository.Save();
                }
            }
        }

        public List<ProjectTasks> FindAll(Users user)
        {
            return ProjectTasksRepository.FindAll(user);
        }
    }
}
