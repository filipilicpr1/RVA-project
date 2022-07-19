namespace Server.Dto.UserDto
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;

    }
}
