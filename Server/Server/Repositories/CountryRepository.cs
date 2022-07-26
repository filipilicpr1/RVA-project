using Microsoft.EntityFrameworkCore;
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
            Country country = _dbContext.Countries.FirstOrDefault(c => String.Equals(c.Name, name));
            return country;
        }

        public async Task<List<Country>> GetAllDistinct()
        {
            List<string> res = await _dbContext.Countries.Select(c => c.Name).Distinct().ToListAsync();
            List<Country> countries = new List<Country>();
            foreach(string countryName in res)
            {
                countries.Add(await _dbContext.Countries.FirstOrDefaultAsync(c => String.Equals(c.Name,countryName)));
            }
            return countries;
        }
    }
}
