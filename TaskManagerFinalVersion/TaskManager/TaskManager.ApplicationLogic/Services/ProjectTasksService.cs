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
            UnitOfWork.Complete();
        }

        public void UpdateTask(ProjectTasks task)
        {
            var foundTask = UnitOfWork.ProjectTasksRepository
                .FindByCondition(t => t.ProjectTasksId == task.ProjectTasksId);
            if (task.Status.Equals(TaskStatus.Done.ToString()) && !foundTask.Status.Equals(TaskStatus.Done.ToString()))
            {
                var foundUser = UnitOfWork.UsersRepository
                    .FindByCondition(u => u.Id == task.UserId);
                if (foundUser != null)
                {
                    foundUser.Score += foundTask.Points;
                    UnitOfWork.UsersRepository.Update(foundUser);
                    UnitOfWork.Complete();
                    var badges = UnitOfWork.BadgesRepository.GetBadge(foundUser);
                    if (badges.Count() > 0)
                    {
                        foreach(var badge in badges)
                        {
                            UserBadges userBadge = new UserBadges
                            {
                                UsersId = foundUser.Id,
                                BadgeId = badge.BadgesId
                            };
                            UnitOfWork.UserBadgesRepository.Create(userBadge);
                            UnitOfWork.Complete();
                        }
                    }

                }
            }
            else
            {
                if (foundTask.Status != task.Status)
                {
                    if (foundTask.Status.Equals(TaskStatus.Done.ToString()))
                    {
                        this.UpdateUserAchivements(foundTask);
                    }
                    foundTask.Status = task.Status;
                }
            }
            foundTask.Description = task.Description;
            foundTask.DueDate = task.DueDate;
            foundTask.Importance = task.Importance;
            foundTask.Name = task.Name;
            foundTask.UserId = task.UserId;

            foundTask.Status = task.Status;
            UnitOfWork.ProjectTasksRepository.Update(foundTask);
            UnitOfWork.Complete();

        }

        public void DeleteTask(ProjectTasks task)
        {
            this.UpdateUserAchivements(task);
            UnitOfWork.ProjectTasksRepository.Delete(task);
            UnitOfWork.Complete();
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
                .SingleOrDefault(t => t.ProjectId == projectId && t.Name.Equals(taskName));
            task.UserId = userId;
            UpdateTask(task);

        }

        public TasksDto GetTaskViewModel(string userId)
        {
            var model = new TasksDto
            {
                ProjectTasks = UnitOfWork.ProjectTasksRepository.FindAllByUserIdOrPM(userId)
            };
            return model;
        }

        public void DeleteTask(TasksDto model)
        {
            foreach (var task in model.ProjectTasks)
            {
                if (task.Selected)
                {
                    var foundTask = UnitOfWork.ProjectTasksRepository
                        .FindByCondition(t => t.ProjectTasksId == task.ProjectTasksId);
                    if (foundTask.UserId != null && foundTask.Status.Equals(TaskStatus.Done.ToString()))
                    {
                        this.UpdateUserAchivements(foundTask);
                    }
                    UnitOfWork.ProjectTasksRepository.Delete(foundTask);
                    UnitOfWork.Complete();
                }
            }
        }

        public List<ProjectTasks> FindAll(Users user)
        {
            return UnitOfWork.ProjectTasksRepository.FindAll(user);
        }

        private void UpdateUserAchivements(ProjectTasks task)
        {
            var user = UnitOfWork.UsersRepository
                            .FindByCondition(u => u.Id == task.UserId);
            if (user != null)
            {
                user.Score -= task.Points;
            }
            UnitOfWork.UsersRepository.Update(user);
            UnitOfWork.Complete();

            // search for the badges that need to be removed
            var badges = UnitOfWork.UserBadgesRepository
                .FindBadges(user).Where(b => b.Badge.NecessaryScore > user.Score)
                .ToList();
            foreach (var badge in badges)
            {
                UnitOfWork.UserBadgesRepository.Delete(badge);
            }
            UnitOfWork.Complete();
        }
    }
}
