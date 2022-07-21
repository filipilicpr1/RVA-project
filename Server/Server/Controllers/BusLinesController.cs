using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.BusLineDto;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/buslines")]
    [ApiController]
    public class BusLinesController : ControllerBase
    {
        private readonly IBusLineService _busLineService;
        public BusLinesController(IBusLineService busLineService)
        {
            _busLineService = busLineService;
        }

        [HttpGet]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<DisplayBusLineDTO> displayBusLineDTOs = await _busLineService.GetAll();
                return Ok(displayBusLineDTOs);
            }
            catch (Exception e)
            {
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
                DetailedBusLineDTO detailedBusLineDTO = await _busLineService.GetById(id);
                return Ok(detailedBusLineDTO);
            }
            catch (Exception e)
            {
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return NotFound(errorDTO);
            }
        }

        [HttpPost("new")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Post([FromBody] NewBusLineDTO newBusLineDTO)
        {
            try
            {
                DisplayBusLineDTO displayBusLineDTO = await _busLineService.CreateBusLine(newBusLineDTO);
                return Ok(displayBusLineDTO);
            }
            catch (Exception e)
            {
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
                DisplayBusLineDTO displayBusLineDTO = await _busLineService.UpdateBusLine(updateBusLineDTO);
                return Ok(displayBusLineDTO);
            }
            catch (Exception e)
            {
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }
    }
}
