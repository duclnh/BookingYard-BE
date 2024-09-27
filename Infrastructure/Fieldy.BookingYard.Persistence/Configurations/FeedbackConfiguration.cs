using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<FeedBack>
    {
        public void Configure(EntityTypeBuilder<FeedBack> builder)
        {
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
                Rating = 5,
                TypeFeedback = TypeFeedback.Customer,
                IsShow = true,
                CreatedAt = DateTime.Now
            });
        }
    }
}
