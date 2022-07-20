using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class CityDataInitializer : ICityDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void InitializeCityData()
        {
            List<City> cities = _unitOfWork.Cities.GetAllSync();
            if(cities.Count != 0 && cities.Find(c => String.Equals(c.Name, "Beograd")) != null)
            {
                return;
            }
            City city = new City() { Name = "Beograd" };
            _unitOfWork.Cities.Add(city);
            city.Country = _unitOfWork.Countries.FindByNameSync("Srbija");
            _unitOfWork.SaveSync();
        }
    }
}
