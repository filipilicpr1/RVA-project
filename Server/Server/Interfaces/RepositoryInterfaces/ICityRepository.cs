using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        City FindByNameSync(string name);
        Task<City> FindByName(string name);
        Task<List<City>> GetAllDistinct();
    }
}
