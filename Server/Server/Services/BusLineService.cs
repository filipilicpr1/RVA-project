﻿using AutoMapper;
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
