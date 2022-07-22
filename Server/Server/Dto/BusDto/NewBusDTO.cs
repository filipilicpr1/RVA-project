namespace Server.Dto.BusDto
{
    public class NewBusDTO
    {
        public string Name { get; set; }
        public string Label { get; set; }   // M2 i M3, klase A,B,I,II,III
        public int BusLineId { get; set; }
        public int ManufacturerId { get; set; }
        public int Timestamp { get; set; }
        public bool Override { get; set; }
    }
}
