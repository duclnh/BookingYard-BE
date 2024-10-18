using System;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Command.DeleteVoucher;

public record class DeleteVoucherCommand(Guid VoucherID) : IRequest<string>
{

}
