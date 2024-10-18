using AutoMapper;
using Fieldy.BookingYard.Application.Features.Voucher.Command.CreateVoucher;
using Fieldy.BookingYard.Application.Features.Voucher.Command.UpdateVoucher;
using Fieldy.BookingYard.Application.Features.Voucher.Queries;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.DTO;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	internal class VoucherProfile : Profile
	{
		public VoucherProfile()
		{
			CreateMap<Voucher, VoucherDTO>()
				.ForMember(dest => dest.VoucherID, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => src.RegisterDate.ToString("dd-MM-yyyy HH:ss:mm")))
				.ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => src.ExpiredDate.ToString("dd-MM-yyyy HH:ss:mm")))
				.ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MM-yyyy HH:ss:mm")))
				.ForMember(dest => dest.IsStarted, opt => opt.MapFrom(src => DateTime.Now >= src.RegisterDate))
				.ForMember(dest => dest.IsEnded, opt => opt.MapFrom(src => src.ExpiredDate < DateTime.Now));

			CreateMap<CreateVoucherCommand, Voucher>();
			CreateMap<Voucher, VoucherHomeDTO>()
				.ForMember(dest => dest.SportName, opt => opt.MapFrom(src => src.Sport.SportName))
				.ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Facility.Name))
				.ForMember(dest => dest.VoucherID, opt => opt.MapFrom(src => src.Id));

			CreateMap<Voucher, VoucherBookingDTO>()
				.ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => src.RegisterDate.ToString("dd-MM-yyyy")))
				.ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => src.ExpiredDate.ToString("dd-MM-yyyy")))
				.ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Facility.Name))
				.ForMember(dest => dest.VoucherID, opt => opt.MapFrom(src => src.Id));

		}
	}
}
