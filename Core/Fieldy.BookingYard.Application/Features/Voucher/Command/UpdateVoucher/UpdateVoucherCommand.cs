using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Command.UpdateVoucher
{
	public class UpdateVoucherCommand : IRequest<string>
	{
		public Guid VoucherID { get; set; }
		public string? VoucherName { get; set; }
		public string? Image { get; set; }
		public int Percentage { get; set; }
		public string? VoucherDescription { get; set; }
		public DateTime RegisterDate { get; set; }
		public DateTime ExpiredDate { get; set; }
		public string? Reason { get; set; }
		public bool Status { get; set; }
		public Guid FacilityID { get; set; }
		public Guid CategorySportID { get; set; }
	}
}
