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
                List<DetailedBusLineDTO> detailedBusLineDTOs = await _busLineService.GetAll();
                return Ok(detailedBusLineDTOs);
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

        [HttpPost]
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

        [HttpPut("{id}/add-city")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> AddCity(int id, [FromBody] AddCityDTO addCityDTO)
        {
            try
            {
                addCityDTO.BusLineId = id;
                await _busLineService.AddCity(addCityDTO);
                SuccessDTO successDTO = new SuccessDTO() { Message = "City added to the bus line" };
                return Ok(successDTO);
            }
            catch (Exception e)
            {
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
                removeCityDTO.BusLineId = id;
                await _busLineService.RemoveCity(removeCityDTO);
                SuccessDTO successDTO = new SuccessDTO() { Message = "City removed from the bus line" };
                return Ok(successDTO);
            }
            catch (Exception e)
            {
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
                deleteBusLineDTO.Id = id;
                await _busLineService.DeleteBusLine(deleteBusLineDTO);
                SuccessDTO successDTO = new SuccessDTO() { Message = "Bus line deleted" };
                return Ok(successDTO);
            }
            catch (Exception e)
            {
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
                DetailedBusLineDTO detailedBusLineDTO = await _busLineService.Duplicate(id);
                return Ok(detailedBusLineDTO);
            }
            catch (Exception e)
            {
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }
    }
}
