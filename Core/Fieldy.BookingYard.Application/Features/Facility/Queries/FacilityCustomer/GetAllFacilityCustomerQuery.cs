using System;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityCustomer;

public record class GetAllFacilityCustomerQuery(RequestParams request, float? longitude, float? latitude) : IRequest<PagingResult<FacilityCustomerDTO>>
{

}