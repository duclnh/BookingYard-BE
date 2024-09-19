using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.GetAllFacilityAdmin;

public record class GetAllFacilityAdminQuery(RequestParams requestParams) : IRequest<PagingResult<FacilityAdminDTO>> {
    
}