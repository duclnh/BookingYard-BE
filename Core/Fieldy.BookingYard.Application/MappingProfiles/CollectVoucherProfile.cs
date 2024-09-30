using AutoMapper;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Commands.CreateCollectVoucher;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	internal class CollectVoucherProfile : Profile
	{
		public CollectVoucherProfile()
		{
			CreateMap<CollectVoucher, CollectVoucherDto>()
				.ForMember(dest => dest.CollectVoucherID, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Voucher.Facility.Name))
				.ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.Voucher.Percentage))
				.ForMember(dest => dest.VoucherName, opt => opt.MapFrom(src => src.Voucher.VoucherName))
				.ForMember(dest => dest.SportName, opt => opt.MapFrom(src => src.Voucher.Sport.SportName))
				.ForMember(dest => dest.FacilityID, opt => opt.MapFrom(src => src.Voucher.FacilityID))
				.ForMember(dest => dest.IsOutDate, opt => opt.MapFrom(src => src.Voucher.ExpiredDate <= DateTime.Now))
				.ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Voucher.RegisterDate.ToString("dd-MM-yyyy")))
				.ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Voucher.ExpiredDate.ToString("dd-MM-yyyy")));

			CreateMap<CreateCollectVoucherCommand, CollectVoucher>();
		}
	}
}
