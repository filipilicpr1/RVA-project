using Server.Dto.CityDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface ICityService
    {
        Task<List<DisplayCityDTO>> GetAll();
        Task<AvailableCityDTO> GetAvailable(int id);
        Task<DisplayCityDTO> CreateCity(NewCityDTO newCityDTO);
        Task DeleteCity(int id);
    }
}
