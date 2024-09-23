using MediatR;

namespace Fieldy.BookingYard.Application.Features.Court.Queries.GetAllCourt;

public record class GetAllCourtQuery(Guid FacilityID) : IRequest<IList<CourtDTO>>
{

}
