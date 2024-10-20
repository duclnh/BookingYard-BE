using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Sport.Queries.DTO;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityDetail
{
	public class GetFacilityDetailQueryHandler : IRequestHandler<GetFacilityDetailQuery, FacilityDetailDTO>
	{
		private readonly IFacilityRepository _facilityRepository;
		private readonly ICourtRepository _courtRepository;
		private readonly IMapper _mapper;
		private readonly IUtilityService _utilityService;

		private readonly IFeedbackRepository _feedbackRepository;

		public GetFacilityDetailQueryHandler(IFacilityRepository facilityRepository, ICourtRepository courtRepository, IMapper mapper, IUtilityService utilityService, IFeedbackRepository feedbackRepository)
		{
			_facilityRepository = facilityRepository;
			_courtRepository = courtRepository;
			_mapper = mapper;
			_utilityService = utilityService;
			_feedbackRepository = feedbackRepository;
		}

		public async Task<FacilityDetailDTO> Handle(GetFacilityDetailQuery request, CancellationToken cancellationToken)
		{
			List<string> images = new List<string>();
			List<string> image360 = new List<string>();

			var facility = await _facilityRepository.Find(x => x.Id == request.facilityID
														&& x.IsDeleted == false, cancellationToken,
														x => x.FacilityTimes,
														x => x.Courts,
														x => x.Images,
														x => x.FeedBacks);

			if (facility == null)
				throw new NotFoundException(nameof(facility), request.facilityID);

			var facilityMapped = _mapper.Map<FacilityDetailDTO>(facility);

			images.Add(facility.Image);

			if (facility.Courts != null)
			{
				foreach (var court in facility.Courts)
				{
					images.Add(court.Image);
					image360.Add(court.Image360);
				}
			}

			if (facility.Images != null)
			{
				foreach (var image in facility.Images)
				{
					if (image.FacilityID == request.facilityID)
					{
						images.Add(image.ImageLink);
					}
				}
			}

			facilityMapped.FacilityImages = images;
			facilityMapped.Facility360s = image360;
			facilityMapped.FacilityMinPrice = facility.GetMinPriceCourt();
			facilityMapped.FacilityMaxPrice = facility.GetMaxPriceCourt();
			facilityMapped.FacilityRating = await _feedbackRepository.GetRatingFacility(facility.Id, cancellationToken);
			facilityMapped.NumberFeedback = await _feedbackRepository.CountAsync(x => x.FacilityID == facility.Id && x.TypeFeedback == TypeFeedback.Customer, cancellationToken);

			var sports = await _courtRepository.GetSports(facility.Id, cancellationToken);

			facilityMapped.Sports = _mapper.Map<IList<SportCreateDTO>>(sports);

			var facilityRatingStar = await _feedbackRepository.GetRatingFacilityStarsCount(facility.Id, cancellationToken);
			facilityMapped.PercentFiveStar = facilityRatingStar.five;
			facilityMapped.PercentFourStar = facilityRatingStar.four;
			facilityMapped.PercentThreeStar = facilityRatingStar.three;
			facilityMapped.PercentTwoStar = facilityRatingStar.two;
			facilityMapped.PercentOneStar = facilityRatingStar.one;
			facilityMapped.OpenDate = facility.GetFacilityOpen();

			return facilityMapped;
		}
	}
}
