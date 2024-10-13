using System;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityCustomer;

public record class GetAllFacilityCustomerQuery(RequestParams RequestParams,
                                                double? Longitude,
                                                double? Latitude,
                                                string? SportID,
                                                string? ProvinceID,
                                                string? DistrictID,
                                                string? Distance,
                                                string? OrderBy,
                                                string? Price) : IRequest<PagingResult<FacilityCustomerDTO>>
{

}
