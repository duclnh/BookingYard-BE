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
		}
	}
}
