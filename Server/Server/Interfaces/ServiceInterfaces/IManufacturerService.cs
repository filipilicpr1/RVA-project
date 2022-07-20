using Server.Dto.ManufacturerDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IManufacturerService
    {
        Task<List<DisplayManufacturerDTO>> GetAllDistinct();
    }
}
