using MediatR;

namespace Fieldy.BookingYard.Application.Features.Holiday.Commands.DeleteHoliday
{
	public class DeleteHolidayCommand : IRequest<string>
	{
		public Guid HolidayID { get; set; }
	}
}
