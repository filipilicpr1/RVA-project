namespace Server.Dto.BusLineDto
{
    public class UpdateBusLineDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string BusLineType { get; set; }
        public int Timestamp { get; set; }
        public bool Override { get; set; }
    }
}
