using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.UserDto;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                AuthDTO authDTO = await _userService.Login(loginDTO);
                return Ok(authDTO);
            } 
            catch(Exception e)
            {
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] NewUserDTO newUserDTO)
        {
            try
            {
                DisplayUserDTO displayUserDTO = await _userService.RegisterUser(newUserDTO);
                return Ok(displayUserDTO);
            }
            catch(Exception e)
            {
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpPut]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Put([FromBody] UpdateUserDTO updateUserDTO)
        {
            try
            {
                updateUserDTO.Username = User.Identity.Name;
                DisplayUserDTO displayUserDTO = await _userService.UpdateUser(updateUserDTO);
                return Ok(displayUserDTO);
            }
            catch(Exception e)
            {
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }

        [HttpGet("logs")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> GetLogs()
        {
            try
            {
                List<LogDTO> logs = await _userService.GetLogs(User.Identity.Name);
                return Ok(logs);
            }
            catch (Exception e)
            {
                ErrorDTO errorDTO = new ErrorDTO() { Message = e.Message };
                return BadRequest(errorDTO);
            }
        }
    }
}
