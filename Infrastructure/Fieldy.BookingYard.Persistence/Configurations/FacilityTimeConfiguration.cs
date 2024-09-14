using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class FacilityTimeConfiguration : IEntityTypeConfiguration<FacilityTime>
	{
		public void Configure(EntityTypeBuilder<FacilityTime> builder)
		{
			builder.Property(x => x.Id).HasColumnName("FacilityTimeID");

			builder.HasData(
				new FacilityTime { 
					Id = 1, 
					Time = "Monday",
					FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4")
                },
                new FacilityTime
                {
                    Id = 2,
                    Time = "Tuesday",
                    FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4")
                },
                new FacilityTime
                {
                    Id = 3,
                    Time = "Wednesday",
                    FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4")
                },
                new FacilityTime
                {
                    Id = 4,
                    Time = "Thursday",
                    FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4")
                },
                new FacilityTime
                {
                    Id = 5,
                    Time = "Friday",
                    FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4")
                },
                new FacilityTime
                {
                    Id = 6,
                    Time = "Saturday",
                    FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4")
                },
                 new FacilityTime
                 {
                     Id = 7,
                     Time = "Sunday",
                     FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4")
                 }

            );
		}
	}
}
