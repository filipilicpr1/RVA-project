namespace Server.Dto.BusLineDto
{
    public class AddCityDTO
    {
        public int BusLineId { get; set; }
        public int CityId { get; set; }
        public int Timestamp { get; set; }
        public bool Override { get; set; }
    }
}
