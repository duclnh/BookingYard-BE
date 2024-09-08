using AutoMapper;
using Fieldy.BookingYard.Application.Features.Discount.Command.CreateDiscount;
using Fieldy.BookingYard.Application.Features.Discount.Command.UpdateDiscount;
using Fieldy.BookingYard.Application.Features.Discount.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	internal class DiscountProfile : Profile
	{
		public DiscountProfile()
		{
			CreateMap<Discount, DiscountDto>()
				.ForMember(dest => dest.DiscountID, opt => opt.MapFrom(src => src.Id)); ;
			CreateMap<CreateDiscountCommand, Discount>();
			CreateMap<UpdateDiscountCommand, Discount>()
				 .AfterMap((src, dest) =>
				 {
					 if (string.IsNullOrEmpty(src.DiscountName))
					 {
						 dest.DiscountName = src.DiscountName;
					 }
					 if (string.IsNullOrEmpty(src.Image))
					 {
						 dest.Image = src.Image;
					 }
					 if (string.IsNullOrEmpty(src.DiscountDescription))
					 {
						 dest.DiscountDescription = src.DiscountDescription;
					 }
					 if (string.IsNullOrEmpty(src.Reason))
					 {
						 dest.Reason = src.Reason;
					 }
				 });
		}
	}
}
