using Server.Interfaces.Prototype;

namespace Server.Models
{
    public class Manufacturer : IDeepCloneable<Manufacturer>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Bus> Buses { get; set; }

        public Manufacturer DeepCopy()
        {
            Manufacturer deepCopy = new Manufacturer();
            deepCopy.Name = this.Name;
            return deepCopy;
        }
    }
}
