using MediatR;

namespace Fieldy.BookingYard.Application.Features.Holiday.Commands.UpdateHoliday
{
	public class UpdateHolidayCommand : IRequest<string>
	{
		public Guid HolidayID { get; set; }
		public Guid FacilityID { get; set; }
		public DateOnly Date { get; set; }
	}
}
