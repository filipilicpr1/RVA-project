using Server.Enums;
using Server.Interfaces.Prototype;

namespace Server.Models
{
    public class BusLine : IDeepCloneable<BusLine>
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public EBusLineType BusLineType { get; set; }
        public List<Bus> Buses { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
        public int Timestamp { get; set; }

        public BusLine DeepCopy()
        {
            BusLine deepCopy = new BusLine();
            deepCopy.Label = this.Label;
            deepCopy.BusLineType = this.BusLineType;
            if(this.Cities != null)
            {
                deepCopy.Cities = this.Cities.ConvertAll(c => c.DeepCopy());
            }
            if(this.Buses != null)
            {
                deepCopy.Buses = this.Buses.ConvertAll(b => b.DeepCopy());
            }
            foreach(Bus bus in deepCopy.Buses)
            {
                bus.BusLine = deepCopy;
            }
            return deepCopy;
        }
    }
}
