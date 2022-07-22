using Server.Dto.UserDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        Task<AuthDTO> Login(LoginDTO loginDTO);
        Task<DisplayUserDTO> RegisterUser(NewUserDTO newUserDTO);
        Task<DisplayUserDTO> UpdateUser(UpdateUserDTO updateUserDTO);
        Task<List<LogDTO>> GetLogs(string username);
    }
}
