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
    }
}
