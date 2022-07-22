using AutoMapper;
using Server.Dto.BusLineDto;
using Server.Enums;
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

        public async Task AddCity(AddCityDTO addCityDTO)
        {
            BusLine busLine = await _unitOfWork.BusLines.FindComplete(addCityDTO.BusLineId);
            if(busLine == null)
            {
                throw new Exception("Invalid bus line");
            }
            City city = await _unitOfWork.Cities.Find(addCityDTO.CityId);
            if(city == null)
            {
                throw new Exception("Invalid city");
            }
            foreach(var c in busLine.Cities)
            {
                if(c.Id == city.Id || String.Equals(c.Name.ToLower(), city.Name.ToLower()))
                {
                    throw new Exception("Bus line already has city with name " + city.Name);
                }
            }
            if (busLine.Timestamp != addCityDTO.Timestamp && !addCityDTO.Override)
            {
                throw new Exception("Conflict");
            }
            busLine.Cities.Add(city);
            busLine.Timestamp++;
            await _unitOfWork.Save();
        }

        public async Task<DisplayBusLineDTO> CreateBusLine(NewBusLineDTO newBusLineDTO)
        {
            BusLine busLine = _mapper.Map<BusLine>(newBusLineDTO);
            ValidationResult result = _busLineValidation.Validate(busLine);
            if (!result.IsValid)
            {
                throw new Exception(result.Message);
            }
            bool busLineExists = await _unitOfWork.BusLines.FindByLabel(busLine.Label) != null;
            if (busLineExists)
            {
                throw new Exception("Bus line with label " + busLine.Label + " already exists");
            }
            await _unitOfWork.BusLines.Add(busLine);
            await _unitOfWork.Save();
            return _mapper.Map<DisplayBusLineDTO>(busLine);
        }

        public async Task DeleteBusLine(DeleteBusLineDTO deleteBusLineDTO)
        {
            BusLine busLine = await _unitOfWork.BusLines.Find(deleteBusLineDTO.Id);
            if(busLine == null)
            {
                throw new Exception("Bus line with id " + deleteBusLineDTO.Id + " does not exist");
            }
            if(busLine.Timestamp != deleteBusLineDTO.Timestamp && !deleteBusLineDTO.Override)
            {
                throw new Exception("Conflict");
            }
            _unitOfWork.BusLines.Remove(busLine);
            await _unitOfWork.Save();
        }

        public async Task<List<DetailedBusLineDTO>> GetAll()
        {
            List<BusLine> busLines = await _unitOfWork.BusLines.GetAllDetailed();
            return _mapper.Map<List<DetailedBusLineDTO>>(busLines);
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

        public async Task RemoveCity(RemoveCityDTO removeCityDTO)
        {
            BusLine busLine = await _unitOfWork.BusLines.FindComplete(removeCityDTO.BusLineId);
            if (busLine == null)
            {
                throw new Exception("Invalid bus line");
            }
            City city = await _unitOfWork.Cities.Find(removeCityDTO.CityId);
            if (city == null)
            {
                throw new Exception("Invalid city");
            }
            if (!busLine.Cities.Contains(city))
            {
                throw new Exception("Bus line does not go through city with id " + city.Id);
            }
            if (busLine.Timestamp != removeCityDTO.Timestamp && !removeCityDTO.Override)
            {
                throw new Exception("Conflict");
            }
            busLine.Cities.Remove(city);
            busLine.Timestamp++;
            await _unitOfWork.Save();
        }

        public async Task<DisplayBusLineDTO> UpdateBusLine(UpdateBusLineDTO updateBusLineDTO)
        {
            BusLine busLine = await _unitOfWork.BusLines.Find(updateBusLineDTO.Id);
            if(busLine == null)
            {
                throw new Exception("Bus line with id " + updateBusLineDTO.Id + " does not exist");
            }
            if(busLine.Timestamp != updateBusLineDTO.Timestamp && !updateBusLineDTO.Override)
            {
                throw new Exception("Conflict");
            }
            BusLine updateBusLine = _mapper.Map<BusLine>(updateBusLineDTO);
            busLine.Label = updateBusLine.Label;
            busLine.BusLineType = updateBusLine.BusLineType;
            ValidationResult result = _busLineValidation.Validate(busLine);
            if (!result.IsValid)
            {
                throw new Exception(result.Message);
            }
            BusLine existingBusLine = await _unitOfWork.BusLines.FindByLabel(busLine.Label);
            bool busLineExists = existingBusLine != null && existingBusLine.Id != updateBusLine.Id;
            if (busLineExists)
            {
                throw new Exception("Bus line with label " + busLine.Label + " already exists");
            }
            busLine.Timestamp++;
            await _unitOfWork.Save();
            return _mapper.Map<DisplayBusLineDTO>(busLine);
        }
    }
}
