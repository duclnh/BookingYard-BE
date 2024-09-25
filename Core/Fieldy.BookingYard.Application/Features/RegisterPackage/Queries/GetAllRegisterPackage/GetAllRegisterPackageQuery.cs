using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.RegisterPackage.Queries.GetAllRegisterPackage
{
	public record GetAllRegisterPackageQuery(RequestParams requestParams) : IRequest<PagingResult<RegisterPackageDto>>
	{
	}
}
