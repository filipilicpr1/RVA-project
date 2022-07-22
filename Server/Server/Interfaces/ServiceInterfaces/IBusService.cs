using Server.Dto.BusDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IBusService
    {
        Task<DisplayBusDTO> CreateBus(NewBusDTO newBusDTO);
        Task DeleteBus(DeleteBusDTO deleteBusDTO);
    }
}
