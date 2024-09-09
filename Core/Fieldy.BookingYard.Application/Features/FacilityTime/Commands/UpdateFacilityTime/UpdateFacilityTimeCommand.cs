using MediatR;

namespace Fieldy.BookingYard.Application.Features.FacilityTime.Commands.UpdateFacilityTime
{
	public class UpdateFacilityTimeCommand : IRequest<string>
	{
		public int FacilityTimeID { get; set; }
		public Guid FacilityID { get; set; }
		public TimeSpan Time { get; set; }
	}
}
