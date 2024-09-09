using Fieldy.BookingYard.Application.Features.Support.Queries.GetAllSupport;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Queries.GetAllPackage
{
    public record GetAllPackageQuery(RequestParams requestParams) : IRequest<PagingResult<PackageDto>>
    {
    }
}
