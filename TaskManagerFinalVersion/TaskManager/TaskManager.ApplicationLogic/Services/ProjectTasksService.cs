using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskManager.DataAccess.DataModels;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.UnitOfWork;
using TaskManager.DataAccess.Helpers;
using TaskManager.ApplicationLogic.Dtos;

namespace TaskManager.ApplicationLogic.Services
{
    public class ProjectTasksService : IProjectTasksService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ProjectTasksService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void AddTask(ProjectTasks task)
        {
            task.User = null;
            task.UserId = null;
            UnitOfWork.ProjectTasksRepository.Create(task);
            UnitOfWork.ProjectTasksRepository.Save();
        }

        public void UpdateTask(ProjectTasks task)
        {
            var foundTask = UnitOfWork.ProjectTasksRepository
                .FindByCondition(t => t.ProjectTasksId == task.ProjectTasksId);
            if (task.Status.Equals(TaskStatus.Done) && !foundTask.Status.Equals(TaskStatus.Done))
            {
                var foundUser = UnitOfWork.UsersRepository.FindByCondition(u => u.Id == task.UserId);
                if (foundUser != null)
                {
                    foundUser.Score += task.Points;
                    UnitOfWork.UsersRepository.Update(foundUser);
                    UnitOfWork.UsersRepository.Save();
                    var badge = UnitOfWork.BadgesRepository.GetBadge(foundUser);
                    if (badge != null)
                    {
                        UserBadges userBadge = new UserBadges
                        {
                            UsersId = foundUser.Id,
                            BadgeId = badge.BadgesId
                        };
                        UnitOfWork.UserBadgesRepository.Create(userBadge);
                    }

                }
            }
            else
            {
                if (foundTask.Status != task.Status)
                {
                    if (foundTask.Status.Equals(TaskStatus.Done))
                    {
                        var user = UnitOfWork.UsersRepository
                            .FindByCondition(u => u.Id == foundTask.UserId);
                        if (user != null)
                        {
                            user.Score -= foundTask.Points;
                        }
                        UnitOfWork.UsersRepository.Update(user);
                        UnitOfWork.UsersRepository.Save();
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
            UnitOfWork.ProjectTasksRepository.Update(foundTask);
            UnitOfWork.ProjectTasksRepository.Save();

        }

        public void DeleteTask(ProjectTasks task)
        {
            UnitOfWork.ProjectTasksRepository.Delete(task);
            UnitOfWork.ProjectTasksRepository.Save();
        }

        public int GetNumberOfUrgentTasks(string userId)
        {
            return UnitOfWork.ProjectTasksRepository.GetNumberOfUrgentTasks(userId);
        }

        public int GetNumberOfFinishedTasks(string userId)
        {
            return UnitOfWork.ProjectTasksRepository.GetNumberOfFinishedTasks(userId);
        }

        public int GetNumberOfUnfinishedTasks(string userId)
        {
            return UnitOfWork.ProjectTasksRepository.GetNumberOfUnfinishedTasks(userId);
        }

        public List<ProjectTasks> SeeTasks(string userId)
        {
            return UnitOfWork.ProjectTasksRepository.SeeTasks(userId);
        }

        public bool ProjectTasksExists(int id)
        {
            return UnitOfWork.ProjectTasksRepository.FindAll()
                .Any(e => e.ProjectId == id);
        }

        public List<ProjectTasks> FindAllByProject(int id)
        {
            return UnitOfWork.ProjectTasksRepository.FindAll()
                .Where(e => e.ProjectId == id)
                .ToList();

        }

        public ProjectTasks FindByCondition(Expression<Func<ProjectTasks, bool>> expression)
        {
            return UnitOfWork.ProjectTasksRepository.FindProjectTaskByCondition(expression);

        }

        public List<ProjectTasks> FindAllByUserIdOrPM(string userid)
        {

            return UnitOfWork.ProjectTasksRepository.FindAllByUserIdOrPM(userid);
        }

        public void AssignTaskToUser(int projectId, string taskName, string userId)
        {
            var task = UnitOfWork.ProjectTasksRepository.FindAll()
                .Where(t => t.ProjectId == projectId)
                .Where(t => t.Name == taskName)
                .SingleOrDefault();
            task.UserId = userId;
            UpdateTask(task);

        }

        public TasksViewModel GetTaskViewModel(string userId)
        {
            var model = new TasksViewModel
            {
                ProjectTasks = UnitOfWork.ProjectTasksRepository.FindAllByUserIdOrPM(userId)
            };
            return model;
        }

        public void DeleteTask(TasksViewModel model)
        {
            foreach (var task in model.ProjectTasks)
            {
                if (task.Selected)
                {
                    var foundTask = UnitOfWork.ProjectTasksRepository
                        .FindByCondition(t => t.ProjectTasksId == task.ProjectTasksId);
                    if (foundTask.UserId != null && foundTask.Status.Equals(TaskStatus.Done))
                    {
                        var user = UnitOfWork.UsersRepository
                            .FindByCondition(u => u.Id == foundTask.UserId);
                        if (user != null)
                        {
                            user.Score -= foundTask.Points;
                        }
                        UnitOfWork.UsersRepository.Update(user);
                        UnitOfWork.UsersRepository.Save();
                    }
                    UnitOfWork.ProjectTasksRepository.Delete(foundTask);
                    UnitOfWork.ProjectTasksRepository.Save();
                }
            }
        }

        public List<ProjectTasks> FindAll(Users user)
        {
            return UnitOfWork.ProjectTasksRepository.FindAll(user);
        }
    }
}
