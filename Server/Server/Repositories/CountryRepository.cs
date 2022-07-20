using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(BusLineDbContext dbContext) : base(dbContext)
        {

        }

        public Country FindByNameSync(string name)
        {
            Country country = _dbContext.Countries.SingleOrDefault(c => String.Equals(c.Name, name));
            return country;
        }
    }
}
