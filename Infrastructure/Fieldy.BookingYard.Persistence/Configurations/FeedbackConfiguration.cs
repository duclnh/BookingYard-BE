using Fieldy.BookingYard.Domain.Entities;
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


        }
	}
}
