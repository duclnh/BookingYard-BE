using MediatR;

namespace Fieldy.BookingYard.Application.Features.CollectVoucher.Commands.CreateCollectVoucher
{
	public class CreateCollectVoucherCommand : IRequest<string>
	{
		public Guid VoucherID { get; set; }
		public Guid UserID { get; set; }
	}
}
