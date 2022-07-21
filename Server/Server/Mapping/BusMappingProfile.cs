using AutoMapper;
using Server.Dto.BusDto;
using Server.Models;

namespace Server.Mapping
{
    public class BusMappingProfile : Profile
    {
        public BusMappingProfile()
        {
            CreateMap<Bus, DisplayBusDTO>().ReverseMap();
        }
    }
}
