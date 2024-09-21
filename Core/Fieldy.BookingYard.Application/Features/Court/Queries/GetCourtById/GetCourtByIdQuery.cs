using MediatR;

namespace Fieldy.BookingYard.Application.Features.Court.Queries.GetCourtById;

public record class GetCourtByIdQuery(int courtId) : IRequest<CourtDetailDTO>
{

}
