using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Country FindByNameSync(string name);
        Task<List<Country>> GetAllDistinct();
    }
}
