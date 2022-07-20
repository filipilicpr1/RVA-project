using AutoMapper;
using Server.Dto.ManufacturerDto;
using Server.Models;

namespace Server.Mapping
{
    public class ManufacturerMappingProfile : Profile
    {
        public ManufacturerMappingProfile()
        {
            CreateMap<Manufacturer, DisplayManufacturerDTO>().ReverseMap();
        }
    }
}
