using System;
using Fieldy.BookingYard.Application.Features.Court.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Court.Queries.GetAllCourtBooking;

public class GetAllCourtBookingQuery : IRequest<IList<CourtBookingDTO>>
{
    public GetAllCourtBookingQuery(Guid facilityID, int sportID, string playDate, string startTime, string endTime)
    {
        this.facilityID = facilityID;
        this.sportID = sportID;
        this.playDate = DateTime.ParseExact(playDate, "dd-MM-yyyy", null); ;
        this.startTime = startTime;
        this.endTime = endTime;
    }

    public Guid facilityID { get; set; }
    public int sportID { get; set; }
    public DateTime playDate { get; set; }
    public string startTime { get; set; }
    public string endTime { get; set; }
}
