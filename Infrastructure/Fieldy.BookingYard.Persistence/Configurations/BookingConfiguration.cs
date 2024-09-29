using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class BookingConfiguration : IEntityTypeConfiguration<Booking>
	{
		public void Configure(EntityTypeBuilder<Booking> builder)
		{
			builder.Property(x => x.Id).HasColumnName("BookingID");
			builder.HasOne(x => x.User).WithMany(u => u.Bookings).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(x => x.Court).WithMany(c => c.Bookings).HasForeignKey(x => x.CourtID);
			builder.HasOne(x => x.Voucher).WithMany(v => v.Bookings).HasForeignKey(x => x.VoucherID);
			builder.Property(x => x.CourtPrice).HasColumnType("decimal(18,2)");
			builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
		}
	}
}
