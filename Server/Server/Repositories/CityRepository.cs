using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(BusLineDbContext dbContext) : base(dbContext)
        {

        }

        public City FindByNameSync(string name)
        {
            City city = _dbContext.Cities.SingleOrDefault(c => String.Equals(c.Name, name));
            return city;
        }
    }
}
