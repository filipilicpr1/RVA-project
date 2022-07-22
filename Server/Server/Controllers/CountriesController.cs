using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.CountryDto;
using Server.Enums;
using Server.Interfaces.Logger;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogging _logger;
        public CountriesController(ICountryService countryService, ILogging logger)
        {
            _countryService = countryService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Getting all countries", ELogType.INFO);
                List<DisplayCountryDTO> displayCountryDTOs = await _countryService.GetAllDistinct();
                return Ok(displayCountryDTOs);
            } 
            catch(Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }
    }
}
