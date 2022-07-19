using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Infrastructure
{
    public class BusLineDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;

        public BusLineDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BusLineDbContext).Assembly);
        }
    }
}
