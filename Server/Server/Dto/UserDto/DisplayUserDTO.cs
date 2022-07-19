using Server.Enums;

namespace Server.Dto.UserDto
{
    public class DisplayUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public EUserType UserType { get; set; }
    }
}
