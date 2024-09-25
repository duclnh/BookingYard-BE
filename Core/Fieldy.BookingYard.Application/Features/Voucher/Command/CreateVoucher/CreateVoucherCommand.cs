using MediatR;
using Microsoft.AspNetCore.Http;

namespace Fieldy.BookingYard.Application.Features.Voucher.Command.CreateVoucher
{
	public class CreateVoucherCommand : IRequest<string>
	{
		public required string VoucherName { get; set; }
		public required IFormFile Image { get; set; }
		public int Percentage { get; set; }
		// public string? VoucherDescription { get; set; }
		public DateTime RegisterDate { get; set; }
		public DateTime ExpiredDate { get; set; }
		public Guid FacilityID { get; set; }
		public int? SportID { get; set; }
	}
}
