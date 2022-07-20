using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Manufacturer>> GetAllDistinct()
        {
            List<string> res = await _dbContext.Manufacturers.Select(c => c.Name).Distinct().ToListAsync();
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            foreach (string manufacturerName in res)
            {
                manufacturers.Add(await _dbContext.Manufacturers.FirstOrDefaultAsync(m => String.Equals(m.Name, manufacturerName)));
            }
            return manufacturers;
        }
    }
}
