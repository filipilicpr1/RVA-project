using Server.Interfaces.Prototype;

namespace Server.Models
{
    public class Country : IDeepCloneable<Country>
    {
        public int Id { get;set; }
        public string Name { get; set; }
        public List<City> Cities { get; set; }

        public Country DeepCopy()
        {
            Country deepCopy = new Country();
            deepCopy.Name = this.Name;
            return deepCopy;

        }
    }
}
