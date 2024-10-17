using System;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.GetAllProvince;

public record class GetAllProvinceQuery : IRequest<IList<int>>
{

}
