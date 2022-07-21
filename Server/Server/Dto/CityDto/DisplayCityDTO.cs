using Server.Dto.CountryDto;

namespace Server.Dto.CityDto
{
    public class DisplayCityDTO
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DisplayCountryDTO Country { get; set; }
    }
}
