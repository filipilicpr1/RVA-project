using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;

namespace Server.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BusLineDbContext _dbContext;

        public GenericRepository(BusLineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            List<T> entities = await _dbContext.Set<T>().ToListAsync();
            return entities;
        }

        public List<T> GetAll()
        {
            List<T> entities = _dbContext.Set<T>().ToList();
            return entities;
        }
    }
}
