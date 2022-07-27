using Microsoft.EntityFrameworkCore;
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

        public async Task<City> FindByName(string name)
        {
            City city = await _dbContext.Cities.FirstOrDefaultAsync(c => String.Equals(c.Name.ToLower(), name.ToLower()));
            return city;
        }

        public City FindByNameSync(string name)
        {
            City city = _dbContext.Cities.FirstOrDefault(c => String.Equals(c.Name, name));
            return city;
        }

        public async Task<List<City>> GetAllComplete()
        {
            List<City> cities = await _dbContext.Cities.Include(c => c.Country).ToListAsync();
            return cities;
        }

        public async Task<List<City>> GetAllDistinct()
        {
            List<string> res = await _dbContext.Cities.Select(c => c.Name.ToLower()).Distinct().ToListAsync();
            List<City> cities = new List<City>();
            foreach (string cityName in res)
            {
                cities.Add(await _dbContext.Cities.Include(c => c.Country).FirstOrDefaultAsync(c => String.Equals(c.Name.ToLower(), cityName.ToLower())));
            }
            return cities;
        }

    }
}
