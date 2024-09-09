using Fieldy.BookingYard.Application.Features.FacilityTime.Queries;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fieldy.BookingYard.Application.Features.FacilityTime.Queries.GetAllFacilityTime
{
	public record GetAllFacilityTimeQuery(RequestParams requestParams) : IRequest<PagingResult<FacilityTimeDto>>
	{
	}
}
