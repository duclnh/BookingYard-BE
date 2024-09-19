using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class FeedbackConfiguration : IEntityTypeConfiguration<FeedBack>
	{
		public void Configure(EntityTypeBuilder<FeedBack> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Id)
				   .HasColumnName("FeedbackID");

            builder.HasOne(x => x.Facility)
                   .WithMany(y => y.FeedBacks)
                   .HasForeignKey(x => x.FacilityID)
                   .OnDelete(DeleteBehavior.Restrict);

			builder.HasData(new FeedBack
			{
					Id = 1,
					UserID = new Guid("57562e90-5835-4461-98f5-565cd760e6da"),
					FacilityID = new Guid("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4"),
					Content = "This football field is so good.",
					Image = "/facility/31fff3b5-663d-49af-b2b6-64fe034f1304.jpg",
					Rating = 5,
					TypeFeedback = TypeFeedback.Customer,
					IsShow = true,
					CreatedAt = DateTime.Parse("2024-09-19 18:00:48.1311317")
			}
			);

			builder.HasData(new FeedBack
			{
				Id = 2,
				UserID = new Guid("03B17C1C-0403-2E08-9ED3-709339DD911B"),
				FacilityID = new Guid("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4"),
				Content = "Good service. I will come back again.",
				Image = "/facility/31fff3b5-663d-49af-b2b6-64fe034f1304.jpg",
				Rating = 5,
				TypeFeedback = TypeFeedback.Customer,
				IsShow = true,
				CreatedAt = DateTime.Parse("2024-09-19 12:11:48.1311317")
			}
			);

			builder.HasData(new FeedBack
			{
				Id = 3,
				UserID = new Guid("03B17C1C-0403-4E08-9ED6-738939DD911B"),
				FacilityID = new Guid("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4"),
				Content = "The quality of the turf is great.",
				Image = "/facility/31fff3b5-663d-49af-b2b6-64fe034f1304.jpg",
				Rating = 5,
				TypeFeedback = TypeFeedback.Customer,
				IsShow = true,
				CreatedAt = DateTime.Parse("2024-09-18 11:42:48.1311317")
			}
			);

		}
	}
}
