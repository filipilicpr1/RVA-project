using Server.Dto.ManufacturerDto;

namespace Server.Dto.BusDto
{
    public class DisplayBusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public DisplayManufacturerDTO Manufacturer { get; set; }
    }
}
