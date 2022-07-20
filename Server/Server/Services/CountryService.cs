using AutoMapper;
using Server.Dto.CountryDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DisplayCountryDTO>> GetAllDistinct()
        {
            List<Country> countries = await _unitOfWork.Countries.GetAllDistinct();
            return _mapper.Map<List<DisplayCountryDTO>>(countries);
        }
    }
}
