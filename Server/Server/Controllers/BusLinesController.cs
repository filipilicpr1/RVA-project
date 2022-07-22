using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.BusLineDto;
using Server.Enums;
using Server.Interfaces.Logger;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/buslines")]
    [ApiController]
    public class BusLinesController : ControllerBase
    {
        private readonly IBusLineService _busLineService;
        private readonly ILogging _logger;
        public BusLinesController(IBusLineService busLineService, ILogging logger)
        {
            _busLineService = busLineService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Getting all bus lines", ELogType.INFO);
                List<DetailedBusLineDTO> detailedBusLineDTOs = await _busLineService.GetAll();
                return Ok(detailedBusLineDTOs);
            }
            catch (Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Getting bus line with id " + id, ELogType.INFO);
                DetailedBusLineDTO detailedBusLineDTO = await _busLineService.GetById(id);
                return Ok(detailedBusLineDTO);
            }
            catch (Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return NotFound(errorDTO);
            }
        }

        [HttpPost]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Post([FromBody] NewBusLineDTO newBusLineDTO)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Creating new bus line", ELogType.INFO);
                DisplayBusLineDTO displayBusLineDTO = await _busLineService.CreateBusLine(newBusLineDTO);
                return Ok(displayBusLineDTO);
            }
            catch (Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPut]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Put([FromBody] UpdateBusLineDTO updateBusLineDTO)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Updating bus line with id " + updateBusLineDTO.Id, ELogType.INFO);
                DisplayBusLineDTO displayBusLineDTO = await _busLineService.UpdateBusLine(updateBusLineDTO);
                return Ok(displayBusLineDTO);
            }
            catch (Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPut("{id}/add-city")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> AddCity(int id, [FromBody] AddCityDTO addCityDTO)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Adding city with id " + addCityDTO.CityId + " to bus line with id " + id, ELogType.INFO);
                addCityDTO.BusLineId = id;
                await _busLineService.AddCity(addCityDTO);
                SuccessDTO successDTO = new SuccessDTO() { Message = "City added to the bus line" };
                return Ok(successDTO);
            }
            catch (Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPut("{id}/remove-city")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> RemoveCity(int id, [FromBody] RemoveCityDTO removeCityDTO)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Removing city with id " + removeCityDTO.CityId + " from bus line with id " + id, ELogType.INFO);
                removeCityDTO.BusLineId = id;
                await _busLineService.RemoveCity(removeCityDTO);
                SuccessDTO successDTO = new SuccessDTO() { Message = "City removed from the bus line" };
                return Ok(successDTO);
            }
            catch (Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Delete(int id, [FromBody] DeleteBusLineDTO deleteBusLineDTO)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Deleting bus line with id " + id, ELogType.INFO);
                deleteBusLineDTO.Id = id;
                await _busLineService.DeleteBusLine(deleteBusLineDTO);
                SuccessDTO successDTO = new SuccessDTO() { Message = "Bus line deleted" };
                return Ok(successDTO);
            }
            catch (Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPost("{id}/duplicate")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Duplicate(int id)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Duplicating bus line with id " + id, ELogType.INFO);
                DetailedBusLineDTO detailedBusLineDTO = await _busLineService.Duplicate(id);
                return Ok(detailedBusLineDTO);
            }
            catch (Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }
    }
}
