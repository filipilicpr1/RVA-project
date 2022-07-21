using AutoMapper;
using Server.Dto.BusLineDto;
using Server.Models;

namespace Server.Mapping
{
    public class BusLineMappingProfile : Profile
    {
        public BusLineMappingProfile()
        {
            CreateMap<BusLine, DisplayBusLineDTO>().ReverseMap();
            CreateMap<BusLine, DetailedBusLineDTO>().ReverseMap();
            CreateMap<BusLine, NewBusLineDTO>().ReverseMap();
        }
    }
}
