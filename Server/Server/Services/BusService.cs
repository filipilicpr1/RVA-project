using AutoMapper;
using Server.Dto.BusDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Services
{
    public class BusService : IBusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidation<Bus> _busValidation;
        public BusService(IUnitOfWork unitOfWork, IMapper mapper, IValidation<Bus> busValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _busValidation = busValidation;
        }

        public async Task<DisplayBusDTO> CreateBus(NewBusDTO newBusDTO)
        {
            Bus bus = _mapper.Map<Bus>(newBusDTO);
            ValidationResult result = _busValidation.Validate(bus);
            if (!result.IsValid)
            {
                throw new Exception(result.Message);
            }
            bool busExists = await _unitOfWork.Buses.FindByName(bus.Name) != null;
            if (busExists)
            {
                throw new Exception("Bus with name " + bus.Name + " already exists");
            }
            BusLine busLine = await _unitOfWork.BusLines.Find(bus.BusLineId);
            bool invalidBusLine = busLine == null;
            if (invalidBusLine)
            {
                throw new Exception("Invalid bus line");
            }
            bool invalidManufacturer = await _unitOfWork.Manufacturers.Find(bus.ManufacturerId) == null;
            if (invalidManufacturer)
            {
                throw new Exception("Invalid manufacturer");
            }
            if(newBusDTO.Timestamp != busLine.Timestamp && !newBusDTO.Override)
            {
                throw new Exception("Conflict");
            }
            await _unitOfWork.Buses.Add(bus);
            busLine.Timestamp++;
            await _unitOfWork.Save();
            return _mapper.Map<DisplayBusDTO>(bus);
        }

        public async Task DeleteBus(DeleteBusDTO deleteBusDTO)
        {
            Bus bus = await _unitOfWork.Buses.Find(deleteBusDTO.Id);
            if(bus == null)
            {
                throw new Exception("Bus with id " + deleteBusDTO.Id + " does not exist");
            }
            BusLine busLine = await _unitOfWork.BusLines.Find(bus.BusLineId);
            if(busLine.Timestamp != deleteBusDTO.Timestamp && !deleteBusDTO.Override)
            {
                throw new Exception("Conflict");
            }
            _unitOfWork.Buses.Remove(bus);
            busLine.Timestamp++;
            await _unitOfWork.Save();
        }
    }
}
