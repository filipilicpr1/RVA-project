namespace Server.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Bus> Buses { get; set; }
    }
}
