using AutoMapper;
using Fieldy.BookingYard.Application.Features.Booking.Commands.CreateBooking;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class BookingProfile : Profile
	{
		public BookingProfile()
		{
			CreateMap<CreateBookingCommand, Domain.Entities.Booking>();
		}
	}
}
