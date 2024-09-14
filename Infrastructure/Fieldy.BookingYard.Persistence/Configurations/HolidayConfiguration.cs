using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
	{
		public void Configure(EntityTypeBuilder<Holiday> builder)
		{
			builder.Property(x => x.Id).HasColumnName("HolidayID");
		}
	}
}
