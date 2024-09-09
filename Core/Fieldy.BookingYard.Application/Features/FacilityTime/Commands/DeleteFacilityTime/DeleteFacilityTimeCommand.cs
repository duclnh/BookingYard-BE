using MediatR;

namespace Fieldy.BookingYard.Application.Features.FacilityTime.Commands.DeleteFacilityTime
{
	public class DeleteFacilityTimeCommand : IRequest<string>
	{
		public int FacilityTimeID { get; set; }
	}
}
