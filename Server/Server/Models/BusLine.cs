using Server.Enums;

namespace Server.Models
{
    public class BusLine
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public EBusLineType BusLineType { get; set; }
        public List<Bus> Buses { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
        public int Timestamp { get; set; }
    }
}
