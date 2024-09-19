using Fieldy.BookingYard.Application.Features.Sport.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Sport.Queries.GetSportCreate;

public record class GetSportCreateQuery : IRequest<IList<SportCreateDTO>>
{

}
