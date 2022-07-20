﻿using Server.Enums;

namespace Server.Dto.UserDto
{
    public class DisplayUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public EUserType UserType { get; set; }
    }
}
