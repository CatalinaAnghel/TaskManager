using TaskManager.DataAccess.DataModels;

namespace TaskManager.DataAccess.Repositories.Abstractions
{
    public interface IBadgesRepository : IRepositoryBase<Badges>
    {
        public Badges GetBadge(Users user);
        public bool BadgeExists(int id);
    }
}
