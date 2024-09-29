using AutoMapper;
using Fieldy.BookingYard.Application.Features.Payment.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class TransactionProfile : Profile
	{
		public TransactionProfile()
		{
			CreateMap<GetVnpayReturnQuery, Transaction>()
				.ForMember(dest => dest.vnp_Amount, opt => opt.MapFrom(src => src.vnp_Amount))
				.AfterMap((src, dest) =>
				{
					if (!string.IsNullOrEmpty(src.vnp_Amount))
					{
						dest.vnp_Amount = int.Parse(src.vnp_Amount) / 100;
					}
				});
		}
	}
}
