using Server.Interfaces.RepositoryInterfaces;

namespace Server.Interfaces.UnitOfWorkInterfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ICountryRepository Countries { get; }
        IManufacturerRepository Manufacturers { get; }
        ICityRepository Cities { get; }
        IBusLineRepository BusLines { get; }
        IBusRepository Buses { get; }
        void SaveSync();
        Task Save();
    }
}
