using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class CountryDataInitializer : ICountryDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<string> countryNames = new List<string>()
        {
            "Srbija","Hrvatska","Bosna i Hercegovina", "Crna gora", "Slovenija", "Madjarska", "Rumunija", "Austrija", "Nemacka", "Italija", "Slovacka"
        };
        public CountryDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void InitializeCountryData()
        {
            List<Country> countries = _unitOfWork.Countries.GetAllSync();
            if(countries.Count == 0)
            {
                _unitOfWork.Countries.AddRangeSync(countryNames.ConvertAll(c => new Country() { Name = c }));
                _unitOfWork.SaveSync();
            }
            if(countries.Find(c => String.Equals(c.Name, "Srbija")) == null)
            {
                _unitOfWork.Countries.AddSync(new Country() { Name = "Srbija" });
                _unitOfWork.SaveSync();
            }
        }
    }
}
