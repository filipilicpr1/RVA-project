using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;

namespace Server.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BusLineDbContext _dbContext;
        public IUserRepository Users { get; }
        public ICountryRepository Countries { get; }
        public IManufacturerRepository Manufacturers { get; }
        public ICityRepository Cities { get; }
        public IBusLineRepository BusLines { get; }
        public IBusRepository Buses { get; }
        public UnitOfWork(BusLineDbContext dbContext, IUserRepository users, ICountryRepository countries, IManufacturerRepository manufacturers, ICityRepository cities, IBusLineRepository busLines ,IBusRepository buses)
        {
            _dbContext = dbContext;
            Users = users;
            Countries = countries;
            Manufacturers = manufacturers;
            Cities = cities;
            BusLines = busLines;
            Buses = buses;
        }

        public void SaveSync()
        {
            _dbContext.SaveChanges();
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
