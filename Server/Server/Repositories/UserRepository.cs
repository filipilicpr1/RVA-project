﻿using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        public UserRepository(BusLineDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<User> FindByUsername(string username)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync<User>(u => String.Equals(u.Username, username));
            return user;
        }
    }
}
