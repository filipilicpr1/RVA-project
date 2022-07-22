using Server.Dto.BusLineDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IBusLineService
    {
        Task<List<DetailedBusLineDTO>> GetAll();
        Task<DetailedBusLineDTO> GetById(int id);
        Task<DisplayBusLineDTO> CreateBusLine(NewBusLineDTO newBusLineDTO);
        Task<DisplayBusLineDTO> UpdateBusLine(UpdateBusLineDTO updateBusLineDTO);
        Task AddCity(AddCityDTO addCityDTO);
        Task RemoveCity(RemoveCityDTO removeCityDTO);
        Task DeleteBusLine(DeleteBusLineDTO deleteBusLineDTO);
    }
}
