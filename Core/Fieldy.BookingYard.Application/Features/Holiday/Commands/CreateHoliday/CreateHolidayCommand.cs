using Fieldy.BookingYard.Domain.Entities;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Holiday.Commands.CreateHoliday
{
	public class CreateHolidayCommand : IRequest<string>
	{
		public Guid FacilityID { get; set; }
		public DateOnly Date { get; set; }
	}
}
