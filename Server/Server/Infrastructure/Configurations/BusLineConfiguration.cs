using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Models;

namespace Server.Infrastructure.Configurations
{
    public class BusLineConfiguration : IEntityTypeConfiguration<BusLine>
    {
        public void Configure(EntityTypeBuilder<BusLine> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Buses)
                   .WithOne(x => x.BusLine)
                   .HasForeignKey(x => x.BusLineId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Cities)
                   .WithMany(x => x.BusLines);
        }
    }
}
