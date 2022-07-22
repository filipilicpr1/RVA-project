using Server.Interfaces.Prototype;

namespace Server.Models
{
    public class City : IDeepCloneable<City>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<BusLine> BusLines { get; set; }

        public City DeepCopy()
        {
            City deepCopy = new City();
            deepCopy.Name = this.Name;
            if(this.Country != null)
            {
                deepCopy.Country = this.Country.DeepCopy();
            }
            return deepCopy;
        }
    }
}
