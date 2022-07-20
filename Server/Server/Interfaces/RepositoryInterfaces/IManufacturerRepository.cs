using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IManufacturerRepository : IGenericRepository<Manufacturer>
    {
        Manufacturer FindByNameSync(string name);
    }
}
