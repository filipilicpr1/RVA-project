using AutoMapper;
using Server.Dto.BusLineDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Services
{
    public class BusLineService : IBusLineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidation<BusLine> _busLineValidation;
        public BusLineService(IUnitOfWork unitOfWork, IMapper mapper, IValidation<BusLine> busLineValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _busLineValidation = busLineValidation;
        }

        public async Task<DisplayBusLineDTO> CreateBusLine(NewBusLineDTO newBusLineDTO)
        {
            BusLine busLine = _mapper.Map<BusLine>(newBusLineDTO);
            ValidationResult result = _busLineValidation.Validate(busLine);
            if (!result.IsValid)
            {
                throw new Exception(result.Message);
            }
            await _unitOfWork.BusLines.Add(busLine);
            await _unitOfWork.Save();
            return _mapper.Map<DisplayBusLineDTO>(busLine);
        }

        public async Task<List<DisplayBusLineDTO>> GetAll()
        {
            List<BusLine> busLines = await _unitOfWork.BusLines.GetAll();
            return _mapper.Map<List<DisplayBusLineDTO>>(busLines);
        }

        public async Task<DetailedBusLineDTO> GetById(int id)
        {
            BusLine busLine = await _unitOfWork.BusLines.GetDetailedById(id);
            if(busLine == null)
            {
                throw new Exception("Bus with id " + id + " does not exist");
            }
            return _mapper.Map<DetailedBusLineDTO>(busLine);
        }
    }
}
