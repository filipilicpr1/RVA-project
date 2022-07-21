using Server.Dto.BusDto;
using Server.Dto.CityDto;

namespace Server.Dto.BusLineDto
{
    public class DetailedBusLineDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string BusLineType { get; set; }
        public List<DisplayBusDTO> Buses { get; set; }
        public List<DisplayCityDTO> Cities { get; set; }
    }
}
