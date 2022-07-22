using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.BusDto;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/buses")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly IBusService _busService;
        public BusesController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpPost]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Post([FromBody] NewBusDTO newBusDTO)
        {
            try
            {
                DisplayBusDTO displayBusDTO = await _busService.CreateBus(newBusDTO);
                return Ok(displayBusDTO);
            }
            catch(Exception e)
            {
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
                deleteBusDTO.Id = id;
                await _busService.DeleteBus(deleteBusDTO);
                SuccessDTO successDTO = new SuccessDTO() { Message = "Bus deleted" };
                return Ok(successDTO);
            }
            catch (Exception e)
            {
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }
    }
}
