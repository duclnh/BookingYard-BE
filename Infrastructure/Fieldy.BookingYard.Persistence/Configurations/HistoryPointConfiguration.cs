using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class HistoryPointConfiguration
	{
		public void Configure(EntityTypeBuilder<HistoryPoint> builder)
		{
			builder.Property(x => x.Id).HasColumnName("HistoryPointID");
		}
	}
}
