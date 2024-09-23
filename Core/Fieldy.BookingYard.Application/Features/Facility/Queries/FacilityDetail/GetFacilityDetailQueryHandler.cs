using AutoMapper;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityDetail
{
	public class GetFacilityDetailQueryHandler : IRequestHandler<GetFacilityDetailQuery, Domain.Entities.Facility>
	{
		private readonly IFacilityRepository _facilityRepository;
		private readonly IPeakHourRepository _peakHourRepository;
		private readonly IHolidayRepository _holidayRepository;
		private readonly IFeedbackRepository _feedbackRepository;
		private readonly IImageRepository _imageRepository;
		private readonly IFacilityTimeRepository _facilityTimeRepository;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public GetFacilityDetailQueryHandler(IFacilityRepository facilityRepository, IPeakHourRepository peakHourRepository, IHolidayRepository holidayRepository, IFeedbackRepository feedbackRepository, IImageRepository imageRepository, IFacilityTimeRepository facilityTimeRepository, IUserRepository userRepository, IMapper mapper)
		{
			_facilityRepository = facilityRepository;
			_peakHourRepository = peakHourRepository;
			_holidayRepository = holidayRepository;
			_feedbackRepository = feedbackRepository;
			_imageRepository = imageRepository;
			_facilityTimeRepository = facilityTimeRepository;
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<Domain.Entities.Facility> Handle(GetFacilityDetailQuery request, CancellationToken cancellationToken)
		{
			var facility = await _facilityRepository.GetFacilityByID(request.facilityID);
			facility.User = await _userRepository.FindByIdAsync(facility.UserID);
			facility.PeakHours = await _peakHourRepository.GetAll(expression: x => x.FacilityID == request.facilityID, cancellationToken);
			facility.Holidays = await _holidayRepository.GetAll(expression: x => x.FacilityID == request.facilityID, cancellationToken);
			facility.FeedBacks = await _feedbackRepository.GetFeedbackByFacilityID(request.facilityID);
			facility.Images = await _imageRepository.GetImagesByFacilityID(request.facilityID);
			facility.FacilityTimes = await _facilityTimeRepository.GetAll(expression: x => x.FacilityID == request.facilityID, cancellationToken);
			return facility;
		}
	}
}
