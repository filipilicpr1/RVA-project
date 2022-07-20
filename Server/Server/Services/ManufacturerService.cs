using AutoMapper;
using Server.Dto.ManufacturerDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ManufacturerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<DisplayManufacturerDTO>> GetAllDistinct()
        {
            List<Manufacturer> manufacturers = await _unitOfWork.Manufacturers.GetAllDistinct();
            return _mapper.Map<List<DisplayManufacturerDTO>>(manufacturers);
        }
    }
}
