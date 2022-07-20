using Server.Dto.CountryDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface ICountryService
    {
        Task<List<DisplayCountryDTO>> GetAllDistinct();
    }
}
