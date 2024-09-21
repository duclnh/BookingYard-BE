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
				.ForMember(dest => dest.CollectVoucherID, opt => opt.MapFrom(src => src.Id));
			CreateMap<CreateCollectVoucherCommand, CollectVoucher>();
		}
	}
}
