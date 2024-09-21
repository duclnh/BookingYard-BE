using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class CollectVoucherConfiguration : IEntityTypeConfiguration<CollectVoucher>
	{
		public void Configure(EntityTypeBuilder<CollectVoucher> builder)
		{
			builder.Property(x => x.Id).HasColumnName("CollectVoucherID");
			builder.HasOne(x => x.User).WithMany(u => u.CollectVouchers).HasForeignKey(x => x.UserID);
			builder.HasOne(x => x.Voucher).WithMany(v => v.CollectVouchers).HasForeignKey(x => x.VoucherID);
		}
	}
}
