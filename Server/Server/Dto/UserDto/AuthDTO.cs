using Server.Enums;

namespace Server.Dto.UserDto
{
    public class AuthDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public EUserType UserType { get; set; }
        public string Token { get; set; }
    }
}
