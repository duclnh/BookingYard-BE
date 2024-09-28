using AutoMapper;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.DeleteFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.UpdateFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Queries;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class FeedbackProfile : Profile
	{
		public FeedbackProfile()
		{
			CreateMap<FeedBack, FeedbackDto>()
				.ForMember(dest => dest.FeedbackID, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
					src.Images != null ? src.Images.Select(img => img.ImageLink) : new List<string>()
				));

			CreateMap<CreateFeedbackCommand, FeedBack>();

			CreateMap<UpdateFeedbackCommand, FeedBack>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FeedbackID))
				.ForMember(dest => dest.FacilityID, opt => opt.Ignore())
				.ForMember(dest => dest.UserID, opt => opt.Ignore())
				.ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
			CreateMap<DeleteFeedbackCommand, FeedBack>()

				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FeedbackID));

			CreateMap<FeedBack, FeedbackFacilityDetailDTO>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
				.ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.User.ImageUrl))
				.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(x => x.ImageLink)))
				.ForMember(dest => dest.CreatedAt,opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MM-yyyy")));

		}
	}
}
