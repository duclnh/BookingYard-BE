using MediatR;

namespace Fieldy.BookingYard.Application.Features.FacilityTime.Commands.CreateFacilityTime
{
	public class CreateFacilityTimeCommand : IRequest<string>
	{
		public Guid FacilityID { get; set; }
		public TimeSpan Time { get; set; }
	}
}
