using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IBusRepository : IGenericRepository<Bus>
    {
        Task<Bus> FindByName(string name);
    }
}
