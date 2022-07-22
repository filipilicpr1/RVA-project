using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.BusDto;
using Server.Enums;
using Server.Interfaces.Logger;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/buses")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly IBusService _busService;
        private readonly ILogging _logger;
        public BusesController(IBusService busService, ILogging logger)
        {
            _busService = busService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Post([FromBody] NewBusDTO newBusDTO)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Creating new bus", ELogType.INFO);
                DisplayBusDTO displayBusDTO = await _busService.CreateBus(newBusDTO);
                return Ok(displayBusDTO);
            }
            catch(Exception e)
            {
                _logger.LogMessage(User.Identity.Name + " : " + e.Message, ELogType.ERROR);
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Delete(int id, [FromBody] DeleteBusDTO deleteBusDTO)
        {
            try
            {
                _logger.LogMessage(User.Identity.Name + " : Deleting bus with id " + id, ELogType.INFO);
                deleteBusDTO.Id = id;
                await _busService.DeleteBus(deleteBusDTO);
                SuccessDTO successDTO = new SuccessDTO() { Message = "Bus deleted" };
                return Ok(successDTO);
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
