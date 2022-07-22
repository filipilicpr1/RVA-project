using AutoMapper;
using Server.Dto.CityDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidation<City> _cityValidation;
        public CityService(IUnitOfWork unitOfWork, IMapper mapper, IValidation<City> cityValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cityValidation = cityValidation;
        }

        public async Task<DisplayCityDTO> CreateCity(NewCityDTO newCityDTO)
        {
            City city = _mapper.Map<City>(newCityDTO);
            city.Country = await _unitOfWork.Countries.Find(newCityDTO.CountryId);
            ValidationResult result = _cityValidation.Validate(city);
            if (!result.IsValid)
            {
                throw new Exception(result.Message);
            }
            bool cityExists = await _unitOfWork.Cities.FindByName(city.Name) != null;
            if(cityExists)
            {
                throw new Exception("City with name " + city.Name + " already exists");
            }
            await _unitOfWork.Cities.Add(city);
            await _unitOfWork.Save();
            return _mapper.Map<DisplayCityDTO>(city);
        }

        public async Task DeleteCity(int id)
        {
            City city = await _unitOfWork.Cities.Find(id);
            if(city == null)
            {
                throw new Exception("City with id " + id + " does not exist");
            }
            _unitOfWork.Cities.Remove(city);
            await _unitOfWork.Save();
        }

        public async Task<List<DisplayCityDTO>> GetAllDistinct()
        {
            List<City> cities = await _unitOfWork.Cities.GetAllDistinct();
            return _mapper.Map<List<DisplayCityDTO>>(cities);
        }

        public async Task<List<DisplayCityDTO>> GetAvailable(int id)
        {
            BusLine busLine = await _unitOfWork.BusLines.FindComplete(id);
            if(busLine == null)
            {
                throw new Exception("Bus line with id " + id + " does not exist");
            }
            List<City> cities = await _unitOfWork.Cities.GetAllDistinct();
            List<City> availableCities = new List<City>();
            foreach(City city in cities)
            {
                bool busLineHasCity = false;
                foreach(City busLineCity in busLine.Cities)
                {
                    if(String.Equals(busLineCity.Name.ToLower(), city.Name.ToLower()))
                    {
                        busLineHasCity = true;
                        break;
                    }
                }
                if (!busLineHasCity)
                {
                    availableCities.Add(city);
                }
            }
            return _mapper.Map<List<DisplayCityDTO>>(availableCities);
        }
    }
}
