using Server.Enums;

namespace Server.Dto.UserDto
{
    public class NewUserDTO
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public EUserType UserType { get; set; }
    }
}
