using AutoMapper;
using Server.Dto.CountryDto;
using Server.Models;

namespace Server.Mapping
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, DisplayCountryDTO>().ReverseMap();
        }
    }
}
