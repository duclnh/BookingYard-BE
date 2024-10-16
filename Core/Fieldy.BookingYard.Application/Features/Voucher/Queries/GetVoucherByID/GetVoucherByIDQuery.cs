using System;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.GetVoucherByID;

public record class GetVoucherByIDQuery(string voucherCode): IRequest<VoucherBookingDTO>
{

}
