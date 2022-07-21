using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.CityDto;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<DisplayCityDTO> displayCityDTOs = await _cityService.GetAllDistinct();
                return Ok(displayCityDTOs);
            }
            catch (Exception e)
            {
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
                DisplayCityDTO displayCityDTO = await _cityService.CreateCity(newCityDTO);
                return Ok(displayCityDTO);
            }
            catch (Exception e)
            {
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
                await _cityService.DeleteCity(id);
                SuccessDTO successDTO = new SuccessDTO() { Message = "City deleted" };
                return Ok(successDTO);
            }
            catch (Exception e)
            {
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
                List<DisplayCityDTO> displayCityDTOs = await _cityService.GetAvailable(busLineId);
                return Ok(displayCityDTOs);
            }
            catch (Exception e)
            {
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }
    }
}
