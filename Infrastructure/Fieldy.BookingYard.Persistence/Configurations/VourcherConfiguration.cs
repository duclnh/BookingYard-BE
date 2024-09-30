using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class VourcherConfiguration : IEntityTypeConfiguration<Voucher>
	{
		public void Configure(EntityTypeBuilder<Voucher> builder)
		{
			builder.Property(x => x.Id).HasColumnName("VoucherID");

			builder.HasData(
				new Voucher
				{
					Id = Guid.Parse("E175DABC-B5A4-4D0E-544D-08DCD4D409D4"),
					VoucherName = "DISCOUNT10",
					Image = "/voucher/voucher10.jfif",
					Percentage = 10,
					VoucherDescription = "Giảm giá 10% trên giá sân",
					RegisterDate = DateTime.Now,
					ExpiredDate = DateTime.Now.AddDays(30),
					Reason = "Khuyến mãi",
					Status = true,
					FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4"),
					Quantity = 100,
					SportID = 1
				},
				new Voucher
				{
					Id = Guid.Parse("E175DABC-B5A4-4D0E-544D-081234D409D4"),
					VoucherName = "DISCOUNT15",
					Image = "/voucher/voucher.jfif",
					Percentage = 10,
					VoucherDescription = "Giảm giá 15% trên giá sân",
					RegisterDate = DateTime.Now,
					ExpiredDate = DateTime.Now.AddDays(30),
					Reason = "Khuyến mãi",
					Status = true,
					FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4"),
					Quantity = 100,
				}
			);
		}
	}
}
