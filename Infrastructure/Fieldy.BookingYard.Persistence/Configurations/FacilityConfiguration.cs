using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class FacilityConfiguration
	{
		public void Configure(EntityTypeBuilder<Facility> builder)
		{
			builder.Property(x => x.FacilityID)
				   .HasColumnName("FacilityID");
		}
	}
}
