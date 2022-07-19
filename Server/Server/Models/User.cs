using Server.Enums;

namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public EUserType UserType { get; set; }
    }
}
