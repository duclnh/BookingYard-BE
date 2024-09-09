using MediatR;

namespace Fieldy.BookingYard.Application.Features.PeakHour.Commands.DeletePeakHour
{
	public class DeletePeakHourCommand : IRequest<string>
	{
		public int PeakHourID { get; set; } 
	}
}
