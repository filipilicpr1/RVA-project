using AutoMapper;
using Server.Dto.CityDto;
using Server.Models;

namespace Server.Mapping
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<City, DisplayCityDTO>().ReverseMap();
        }
    }
}
