using Server.Enums;

namespace Server.Dto.UserDto
{
    public class NewUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
    }
}
