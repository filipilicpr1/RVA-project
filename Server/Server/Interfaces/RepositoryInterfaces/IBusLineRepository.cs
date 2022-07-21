using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IBusLineRepository : IGenericRepository<BusLine>
    {
        BusLine FindByLabelSync(string label);
        Task<BusLine> FindByLabel(string label);
        Task<BusLine> GetDetailedById(int id);
    }
}
