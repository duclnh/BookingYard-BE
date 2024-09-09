using MediatR;

namespace Fieldy.BookingYard.Application.Features.PeakHour.Commands.UpdatePeakHour
{
	public class UpdatePeakHourCommand : IRequest<string>
	{
		public int PeakHourID { get; set; }
		public Guid FacilityID { get; set; }
		public TimeSpan Time { get; set; }
	}
}
