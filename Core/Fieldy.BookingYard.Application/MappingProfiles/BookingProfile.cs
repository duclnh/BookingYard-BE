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
			CreateMap<CreateBookingCommand, Booking>()
				.ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.BookingDate, "dd-MM-yyyy", null)));

			CreateMap<Booking, BookingDetailDto>()
				.ForMember(dest => dest.BookingID, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.IsCheckIn, opt => opt.MapFrom(src => src.IsCheckin))
				.ForMember(dest => dest.FacilityID, opt => opt.MapFrom(src => src.Court != null ? src.Court.FacilityID : Guid.Empty))
				.ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Court != null ? (src.Court.Facility != null ? src.Court.Facility.Name : null) : null))
				.ForMember(dest => dest.FacilityLogo, opt => opt.MapFrom(src => src.Court != null ? (src.Court.Facility != null ? src.Court.Facility.Logo : null) : null))
				.ForMember(dest => dest.FacilityImage, opt => opt.MapFrom(src => src.Court != null ? (src.Court.Facility != null ? src.Court.Facility.Image : null) : null))
				.ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.Court != null ? (src.Court.Facility != null ? src.Court.Facility.FullAddress : null) : null))
				.ForMember(dest => dest.CourtName, opt => opt.MapFrom(src => src.Court != null ? src.Court.CourtName : null))
				.ForMember(dest => dest.CourtImage, opt => opt.MapFrom(src => src.Court != null ? src.Court.Image : null))
				.ForMember(dest => dest.Court360, opt => opt.MapFrom(src => src.Court != null ? src.Court.Image360 : null))
				.ForMember(dest => dest.CourtType, opt => opt.MapFrom(src => src.Court != null ? src.Court.NumberPlayer.ToString() : null))
				.ForMember(dest => dest.BookingName, opt => opt.MapFrom(src => src.FullName))
				.ForMember(dest => dest.BookingPhone, opt => opt.MapFrom(src => src.Phone))
	            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToString(@"hh\:mm")))
			    .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToString(@"hh\:mm")))
				.ForMember(dest => dest.PlayDate, opt => opt.MapFrom(src => src.BookingDate.ToString("dd-MM-yyyy")))
				.ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MM-yyyy HH:mm")))
				.ForMember(dest => dest.VoucherName, opt => opt.MapFrom(src => src.Voucher != null ? src.Voucher.VoucherName : null))
				.ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.Voucher != null ? src.Voucher.Percentage : 0))
				.ForMember(dest => dest.VoucherCode, opt => opt.MapFrom(src => src.Voucher != null ? src.Voucher.Code : null))
				.ForMember(dest => dest.VoucherFacility, opt => opt.MapFrom(src => src.Voucher != null ? src.Voucher.FacilityID.ToString() : null))
				.ForMember(dest => dest.VoucherStartDate, opt => opt.MapFrom(src => src.Voucher != null ? src.Voucher.RegisterDate.ToString("dd-MM-yyyy") : null))
				.ForMember(dest => dest.VoucherEndDate, opt => opt.MapFrom(src => src.Voucher != null ? src.Voucher.ExpiredDate.ToString("dd-MM-yyyy") : null))
				.ForMember(dest => dest.VoucherSport, opt => opt.MapFrom(src => src.Voucher != null ? src.Voucher.Sport.SportName : null))
				.ForMember(dest => dest.SportName, opt => opt.MapFrom(src => src.Court.Sport.SportName))
				.ForMember(dest => dest.NumberPlayer, opt => opt.MapFrom(src => src.Court.NumberPlayer));

			CreateMap<Booking, CustomerBooking>()
				.ForMember(dest => dest.BookingID, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.PaymentCode, opt => opt.MapFrom(src => src.PaymentCode))
				.ForMember(dest => dest.FacilityID, opt => opt.MapFrom(src => src.Court != null ? src.Court.FacilityID : Guid.Empty))
				.ForMember(dest => dest.FacilityLogo, opt => opt.MapFrom(src => src.Court != null ? (src.Court.Facility != null ? src.Court.Facility.Logo : null) : null))
				.ForMember(dest => dest.FacilityImage, opt => opt.MapFrom(src => src.Court != null ? (src.Court.Facility != null ? src.Court.Facility.Image : null) : null))
				.ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Court != null ? (src.Court.Facility != null ? src.Court.Facility.Name : null) : null))
				.ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
				.ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToString("HH:mm")))
				.ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToString("HH:mm")))
				.ForMember(dest => dest.PlayDate, opt => opt.MapFrom(src => src.BookingDate.ToString("dd-MM-yyyy")))
				.ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MM-yyyy")))
				.ForMember(dest => dest.IsCheckIn, opt => opt.MapFrom(src => src.IsCheckin));
		}
	}
}
