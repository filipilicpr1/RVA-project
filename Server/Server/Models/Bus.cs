namespace Server.Models
{
    public class Bus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }   // M2 i M3, klase A,B,I,II,III
        public int BusLineId { get; set; }
        public BusLine BusLine { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
