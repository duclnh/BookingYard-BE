using AutoMapper;
using Fieldy.BookingYard.Application.Features.Booking.Commands.CreateBooking;
using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class BookingProfile : Profile
	{
		public BookingProfile()
		{
			CreateMap<CreateBookingCommand, Booking>();
			CreateMap<Booking, BookingDetailDto>()
				.ForMember(dest => dest.BookingID, opt => opt.MapFrom(src => src.Id));
		}
	}
}
