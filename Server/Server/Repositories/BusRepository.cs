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
    }
}
