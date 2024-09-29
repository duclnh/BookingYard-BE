using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.GetAllVoucherFacility;

public record class GetAllVoucherFacilityQuery(RequestParams requestParams, Guid facilityID) : IRequest<PagingResult<VoucherDTO>>
{

}
