using System;
using System.Threading.Tasks;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Repositories;
using TaskManager.DataAccess.Repositories.Abstractions;

namespace TaskManager.DataAccess.UnitOfWork
{
    public sealed class UnitOfWork: IUnitOfWork{
        private readonly TaskManagerDbContext dbContext;

        public UnitOfWork(TaskManagerDbContext dbContext){
            this.dbContext = dbContext;
        }

        private IProjectsRepository Project;
        private IBadgesRepository Badge;
        private IProjectTasksRepository ProjectTask;
        private ITeamsRepository Team;
        private IUsersRepository User;
        private IUserTeamsRepository UserTeam;
        private IUserBadgesRepository UserBadge;

        public IProjectsRepository ProjectsRepository
        {
            get
            {
                if (this.Project == null)
                {
                    this.Project = new ProjectsRepository(dbContext);
                }
                return this.Project;
            }
        }

        public IBadgesRepository BadgesRepository
        {
            get
            {
                if (this.Badge == null)
                {
                    this.Badge = new BadgesRepository(dbContext);
                }
                return this.Badge;
            }
        }

        public IProjectTasksRepository ProjectTasksRepository
        {
            get
            {
                if (this.ProjectTask == null)
                {
                    this.ProjectTask = new ProjectTasksRepository(dbContext);
                }
                return this.ProjectTask;
            }
        }

        public ITeamsRepository TeamsRepository
        {
            get
            {
                if (this.Team == null)
                {
                    this.Team = new TeamsRepository(dbContext);
                }
                return this.Team;
            }
        }

        public IUsersRepository UsersRepository
        {
            get
            {
                if (this.User == null)
                {
                    this.User = new UsersRepository(dbContext);
                }
                return this.User;
            }
        }

        public IUserTeamsRepository UserTeamsRepository
        {
            get
            {
                if (this.UserTeam == null)
                {
                    this.UserTeam = new UserTeamsRepository(dbContext);
                }
                return this.UserTeam;
            }
        }

        public IUserBadgesRepository UserBadgesRepository
        {
            get
            {
                if (this.UserBadge == null)
                {
                    this.UserBadge = new UserBadgesRepository(dbContext);
                }
                return this.UserBadge;
            }
        }

        public int Complete()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}