using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class PeakHourConfiguration : IEntityTypeConfiguration<PeakHour>
	{
		public void Configure(EntityTypeBuilder<PeakHour> builder)
		{
			builder.Property(x => x.Id).HasColumnName("PeakHourID");
			builder.HasData(
				new PeakHour
				{
					Id = 1,
					Time = new TimeSpan(8, 0, 0),
					FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4")
                },
                new PeakHour
                {
                    Id = 2,
                    Time = new TimeSpan(17, 0, 0),
                    FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4")
                }
            ) ;
		}
	}
}
