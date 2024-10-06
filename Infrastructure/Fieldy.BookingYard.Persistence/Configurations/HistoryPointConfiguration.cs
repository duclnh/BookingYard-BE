using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class HistoryPointConfiguration : IEntityTypeConfiguration<HistoryPoint>
	{
		public void Configure(EntityTypeBuilder<HistoryPoint> builder)
		{
			builder.Property(x => x.Id).HasColumnName("HistoryPointID").ValueGeneratedOnAdd();

			builder.HasData(
				new HistoryPoint
				{
					Id = 1,
					Point = 20,
					UserID = new Guid("03B17C1C-0403-2E08-9ED3-709339DD911C"),
					CreatedAt = DateTime.Now
				},
				new HistoryPoint
				{
					Id = 2,
					Point = 50,
					UserID = new Guid("03B17C1C-0403-2E08-9ED3-709339DD911B"),
					CreatedAt = DateTime.Now
				}
			);
		}
	}
}
