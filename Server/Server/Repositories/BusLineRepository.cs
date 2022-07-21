using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class BusLineRepository : GenericRepository<BusLine>, IBusLineRepository
    {
        public BusLineRepository(BusLineDbContext dbContext) : base(dbContext)
        {

        }

        public BusLine FindByLabelSync(string label)
        {
            BusLine busLine = _dbContext.BusLines.SingleOrDefault(b => String.Equals(b.Label,label));
            return busLine;
        }

        public async Task<BusLine> GetDetailedById(int id)
        {
            BusLine busLine = await _dbContext.BusLines.Include(b => b.Buses).Include(b => b.Cities).FirstOrDefaultAsync(b => b.Id == id); 
            if(busLine == null)
            {
                return null;
            }
            foreach(City city in busLine.Cities)
            {
                city.Country = await _dbContext.Countries.FindAsync(city.CountryId);
            }
            foreach(Bus bus in busLine.Buses)
            {
                bus.Manufacturer = await _dbContext.Manufacturers.FindAsync(bus.ManufacturerId);
            }
            return busLine;
        }
    }
}
