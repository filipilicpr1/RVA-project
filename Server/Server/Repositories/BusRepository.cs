using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class BusRepository : GenericRepository<Bus>, IBusRepository
    {
        public BusRepository(BusLineDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Bus> FindByName(string name)
        {
            Bus bus = await _dbContext.Buses.SingleOrDefaultAsync(b => String.Equals(b.Name.ToLower(), name.ToLower()));
            return bus;
        }
    }
}
