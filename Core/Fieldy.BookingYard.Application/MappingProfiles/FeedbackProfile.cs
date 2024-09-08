using AutoMapper;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.DeleteFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.UpdateFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class FeedbackProfile : Profile
	{
		public FeedbackProfile()
		{
			CreateMap<FeedBack, FeedbackDto>()
				.ForMember(dest => dest.FeedbackID, opt => opt.MapFrom(src => src.Id));
			CreateMap<CreateFeedbackCommand, FeedBack>();
			CreateMap<UpdateFeedbackCommand, FeedBack>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FeedbackID));
			CreateMap<DeleteFeedbackCommand, FeedBack>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FeedbackID));
		}
	}
}
