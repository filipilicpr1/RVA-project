using Server.Interfaces.RepositoryInterfaces;

namespace Server.Interfaces.UnitOfWorkInterfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        void Save();
        Task SaveAsync();
    }
}
