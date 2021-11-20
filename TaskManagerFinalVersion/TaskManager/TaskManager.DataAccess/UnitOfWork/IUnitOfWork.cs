using System;
using System.Threading.Tasks;
using TaskManager.DataAccess.Repositories.Abstractions;

namespace TaskManager.DataAccess.UnitOfWork{
    public interface IUnitOfWork: IDisposable{
        IProjectsRepository ProjectsRepository { get; }
        IProjectTasksRepository ProjectTasksRepository { get; }
        IUsersRepository UsersRepository { get; }
        IUserBadgesRepository UserBadgesRepository { get; }
        IUserTeamsRepository UserTeamsRepository { get; }
        ITeamsRepository TeamsRepository { get; }
        IBadgesRepository BadgesRepository { get; }

        Task<int> CompleteAsync();
        int Complete();
        public void Dispose();
    }
}
