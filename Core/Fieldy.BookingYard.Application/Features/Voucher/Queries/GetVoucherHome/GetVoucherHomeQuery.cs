using System;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.GetVoucherHome;

public class GetVoucherHomeQuery : IRequest<IList<VoucherHomeDTO>>
{

}
