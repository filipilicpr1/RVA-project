using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class ManufacturerRepository : GenericRepository<Manufacturer>,IManufacturerRepository
    {
        public ManufacturerRepository(BusLineDbContext dbContext) : base(dbContext)
        {

        }

        public Manufacturer FindByNameSync(string name)
        {
            Manufacturer manufacturer = _dbContext.Manufacturers.SingleOrDefault(m => String.Equals(m.Name,name));
            return manufacturer;
        }
    }
}
