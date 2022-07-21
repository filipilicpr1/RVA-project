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

        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void AddRangeSync(List<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public void AddSync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public async Task<T> Find(int id)
        {
            T entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<List<T>> GetAll()
        {
            List<T> entities = await _dbContext.Set<T>().ToListAsync();
            return entities;
        }

        public List<T> GetAllSync()
        {
            List<T> entities = _dbContext.Set<T>().ToList();
            return entities;
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
