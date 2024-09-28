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
				.ForMember(dest => dest.VoucherID, opt => opt.MapFrom(src => src.Id));
			CreateMap<CreateVoucherCommand, Voucher>();
			CreateMap<UpdateVoucherCommand, Voucher>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VoucherID))
				.AfterMap((src, dest) =>
				{
					if (!string.IsNullOrEmpty(src.VoucherName))
					{
						dest.VoucherName = src.VoucherName;
					}
					if (!string.IsNullOrEmpty(src.Image))
					{
						dest.Image = src.Image;
					}
					if (!string.IsNullOrEmpty(src.VoucherDescription))
					{
						dest.VoucherDescription = src.VoucherDescription;
					}
					if (!string.IsNullOrEmpty(src.Reason))
					{
						dest.Reason = src.Reason;
					}
					if (src.Percentage != 0)
					{
						dest.Percentage = src.Percentage;
					}
					if (src.RegisterDate != DateTime.MinValue)
					{
						dest.RegisterDate = src.RegisterDate;
					}
					if (src.ExpiredDate != DateTime.MinValue)
					{
						dest.ExpiredDate = src.ExpiredDate;
					}
					if (src.FacilityID != Guid.Empty)
					{
						dest.FacilityID = src.FacilityID;
					}
				});

			CreateMap<Voucher, VoucherHomeDTO>()
				.ForMember(dest => dest.SportName, opt => opt.MapFrom(src => src.Sport.SportName));
		}
	}
}
