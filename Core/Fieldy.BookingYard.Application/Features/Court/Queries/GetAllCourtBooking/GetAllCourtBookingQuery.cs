using System;
using Fieldy.BookingYard.Application.Features.Court.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Court.Queries.GetAllCourtBooking;

public record class GetAllCourtBookingQuery(Guid facilityID,int sportID, DateTime datePlay, TimeSpan startTime, TimeSpan endTime) : IRequest<IList<CourtBookingDTO>> 
{

}
