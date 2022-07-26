using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.CityDto;
using Server.Enums;
using Server.Interfaces.Logger;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ILogging _logger;
        public CitiesController(ICityService cityService, ILogging logger)
        {
            _cityService = cityService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Getting all cities", ELogType.INFO);
                List<DisplayCityDTO> displayCityDTOs = await _cityService.GetAllDistinct();
                return Ok(displayCityDTOs);
            }
            catch (Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPost]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Post(NewCityDTO newCityDTO)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Creating new city", ELogType.INFO);
                DisplayCityDTO displayCityDTO = await _cityService.CreateCity(newCityDTO);
                return Ok(displayCityDTO);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Deleting city with id " + id, ELogType.INFO);
                await _cityService.DeleteCity(id);
                SuccessDTO successDTO = new SuccessDTO() { Message = "City deleted" };
                return Ok(successDTO);
            }
            catch (Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return NotFound(errorDTO);
            }
        }

        [HttpGet("available/{busLineId}")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> GetAvailable(int busLineId)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Getting all available cities for bus line with id " + busLineId, ELogType.INFO);
                AvailableCityDTO availableCityDTO = await _cityService.GetAvailable(busLineId);
                return Ok(availableCityDTO);
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
