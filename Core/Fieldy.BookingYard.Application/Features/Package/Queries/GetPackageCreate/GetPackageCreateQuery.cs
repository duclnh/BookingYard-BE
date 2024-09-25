using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Queries.GetPackageCreate
{
    public record GetPackageCreateQuery : IRequest<IList<PackageCreate>>
    {
    }
}
