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


			var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
				d => d.ToDateTime(TimeOnly.MinValue),
				d => DateOnly.FromDateTime(d));

			builder.Property(h => h.Date)
				.HasConversion(dateOnlyConverter);
		}
	}
}
