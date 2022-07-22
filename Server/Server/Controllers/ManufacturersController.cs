using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.ManufacturerDto;
using Server.Enums;
using Server.Interfaces.Logger;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/manufacturers")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly ILogging _logger;

        public ManufacturersController(IManufacturerService manufacturerService, ILogging logger)
        {
            _manufacturerService = manufacturerService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Getting all manufacturers", ELogType.INFO);
                List<DisplayManufacturerDTO> displayManufacturerDTOs = await _manufacturerService.GetAllDistinct();
                return Ok(displayManufacturerDTOs);
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
