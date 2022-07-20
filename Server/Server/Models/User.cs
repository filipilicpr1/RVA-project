﻿using Server.Enums;

namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public EUserType UserType { get; set; }
    }
}
