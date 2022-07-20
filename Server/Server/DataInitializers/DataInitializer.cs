using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;

namespace Server.DataInitializers
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserDataInitializer _userDataInitializer;
        private readonly ICountryDataInitializer _countryDataInitializer;
        private readonly IManufacturerDataInitializer _manufacturerDataInitializer;
        private readonly ICityDataInitializer _cityDataInitializer;
        private readonly IBusDataInitializer _busDataInitializer;
        private readonly IBusLineDataInitializer _busLineDataInitializer;

        public DataInitializer(IUserDataInitializer userDataInitializer, ICountryDataInitializer countryDataInitializer, IManufacturerDataInitializer manufacturerDataInitializer, ICityDataInitializer cityDataInitializer, IBusLineDataInitializer busLineDataInitializer,IBusDataInitializer busDataInitializer)
        {
            _userDataInitializer = userDataInitializer;
            _countryDataInitializer = countryDataInitializer;
            _manufacturerDataInitializer = manufacturerDataInitializer;
            _cityDataInitializer = cityDataInitializer;
            _busLineDataInitializer = busLineDataInitializer;
            _busDataInitializer = busDataInitializer;
        }

        public void InitializeData()
        {
            _userDataInitializer.InitializeUserData();
            _countryDataInitializer.InitializeCountryData();
            _manufacturerDataInitializer.InitializeManufacturerData();
            _cityDataInitializer.InitializeCityData();
            _busLineDataInitializer.InitializeBusLineData();
            _busDataInitializer.InitializeBusData();
        }
    }
}
