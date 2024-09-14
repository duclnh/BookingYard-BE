using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class HistoryPointConfiguration : IEntityTypeConfiguration<HistoryPoint>
	{
		public void Configure(EntityTypeBuilder<HistoryPoint> builder)
		{
			builder.Property(x => x.Id).HasColumnName("HistoryPointID");

			builder.HasData(
				new HistoryPoint
				{
					Id = 1,
					Point = -20,
					UserID = new Guid("57562e90-5835-4461-98f5-565cd760e6da"),
					CreatedAt = DateTime.Now
				},
				new HistoryPoint
				{
					Id = 2,
					Point = 50,
					UserID = new Guid("57562e90-5835-4461-98f5-565cd760e6da"),
					CreatedAt = DateTime.Now
				}
			);
		}
	}
}
