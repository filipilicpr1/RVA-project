namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<List<T>> GetAll();
        void AddSync(T entity);
        List<T> GetAllSync();
        void AddRangeSync(List<T> entities);
        Task<T> Find(int id);
        void Remove(T entity);
    }
}
