using Fieldy.BookingYard.Domain.Entities;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.PeakHour.Commands.CreatePeakHour
{
	public class CreatePeakHourCommand : IRequest<string>
	{
		public Guid FacilityID { get; set; }
		public TimeSpan Time { get; set; }
	}
}
