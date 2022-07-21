using Server.Dto.BusLineDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IBusLineService
    {
        Task<List<DisplayBusLineDTO>> GetAll();
        Task<DetailedBusLineDTO> GetById(int id);
        Task<DisplayBusLineDTO> CreateBusLine(NewBusLineDTO newBusLineDTO);
        Task<DisplayBusLineDTO> UpdateBusLine(UpdateBusLineDTO updateBusLineDTO);
    }
}
