using MediatR;

namespace Fieldy.BookingYard.Application.Features.CollectVoucher.Commands.UpdateCollectVoucher
{
	public class UpdateCollectVoucherCommand : IRequest<string>
	{
		public Guid CollectVoucherID { get; set; }
	}
}
