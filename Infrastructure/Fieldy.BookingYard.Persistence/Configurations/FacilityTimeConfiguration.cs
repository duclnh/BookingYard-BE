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
			builder.Property(p => p.Time)
			.HasColumnType("timestamp");
		}
	}
}
