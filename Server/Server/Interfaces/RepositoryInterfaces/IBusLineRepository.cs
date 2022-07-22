using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IBusLineRepository : IGenericRepository<BusLine>
    {
        BusLine FindByLabelSync(string label);
        Task<BusLine> FindByLabel(string label);
        Task<BusLine> FindComplete(int id);
        Task<BusLine> GetDetailedById(int id);
        Task<List<BusLine>> GetAllDetailed();
    }
}
