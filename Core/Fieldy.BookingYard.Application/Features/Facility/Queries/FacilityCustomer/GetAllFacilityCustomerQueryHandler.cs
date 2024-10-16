using System.Linq.Expressions;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityCustomer;

public class GetAllFacilityCustomerQueryHandler : IRequestHandler<GetAllFacilityCustomerQuery, PagingResult<FacilityCustomerDTO>>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ICourtRepository _courtRepository;


    public GetAllFacilityCustomerQueryHandler(IFacilityRepository facilityRepository, IFeedbackRepository feedbackRepository, ICourtRepository courtRepository)
    {
        _facilityRepository = facilityRepository;
        _feedbackRepository = feedbackRepository;
        _courtRepository = courtRepository;
    }

    public async Task<PagingResult<FacilityCustomerDTO>> Handle(GetAllFacilityCustomerQuery request, CancellationToken cancellationToken)
    {
        List<Expression<Func<Domain.Entities.Facility, bool>>> expressions = new List<Expression<Func<Domain.Entities.Facility, bool>>>
        {
            x => x.IsDeleted == false
        };

        Func<IQueryable<Domain.Entities.Facility>, IOrderedQueryable<Domain.Entities.Facility>> orderBy = x => x.OrderByDescending(x => x.CreatedAt); ;

        if (!string.IsNullOrEmpty(request.ProvinceID))
        {
            expressions.Add(x => x.ProvinceID.ToString() == request.ProvinceID);
        }

        if (!string.IsNullOrEmpty(request.DistrictID))
        {
            expressions.Add(x => x.DistrictID.ToString() == request.DistrictID);
        }

        if (!string.IsNullOrEmpty(request.RequestParams.Search))
        {
            string searchTerm = request.RequestParams.Search.ToLower().Trim();
            expressions.Add(x => x.Name.ToLower().Contains(searchTerm)
                                || x.FullAddress.ToLower().Contains(searchTerm));
        }

        if (!string.IsNullOrEmpty(request.SportID))
        {
            expressions.Add(x => x.Courts.Any(x => x.SportID.ToString() == request.SportID));
        }

        if (!string.IsNullOrEmpty(request.Price) &&
              decimal.TryParse(request.Price, out decimal priceSearch))
        {
            expressions.Add(x => x.Courts.Min(x => x.CourtPrice) < priceSearch);
        }

        switch (request.OrderBy)
        {
            case "priceAsc":
                orderBy = x => x.OrderBy(f => f.Courts.Min(c => c.CourtPrice));
                break;

            case "priceDesc":
                orderBy = x => x.OrderByDescending(f => f.Courts.Max(c => c.CourtPrice));
                break;

        }

        Expression<Func<Domain.Entities.Facility, bool>>[] expressionArray = expressions.ToArray();

        var listFacility = await _facilityRepository.FindAllFacility(
            expressions: expressionArray,
            orderBy: orderBy,
            cancellationToken: cancellationToken,
            includes: x => x.Courts
        );

        List<FacilityCustomerDTO> facilityCustomers = new List<FacilityCustomerDTO>();
        foreach (var facility in listFacility)
        {
            facilityCustomers.Add(
                new FacilityCustomerDTO
                {
                    FacilityID = facility.Id,
                    FacilityImage = facility.Image,
                    FacilityName = facility.Name,
                    FacilityAddress = facility.FullAddress,
                    FacilityRating = await _feedbackRepository.GetRatingFacility(facility.Id, cancellationToken),
                    FacilityMinPrice = await _courtRepository.GetMinPriceCourt(facility.Id, cancellationToken),
                    FacilityMaxPrice = await _courtRepository.GetMaxPriceCourt(facility.Id, cancellationToken),
                    FacilityDistance = CalculateDistance(facility.Latitude, facility.Longitude, request.Latitude ?? 0, request.Longitude ?? 0)
                }
            );
            facilityCustomers.Add(
               new FacilityCustomerDTO
               {
                   FacilityID = facility.Id,
                   FacilityImage = facility.Image,
                   FacilityName = facility.Name,
                   FacilityAddress = facility.FullAddress,
                   FacilityRating = await _feedbackRepository.GetRatingFacility(facility.Id, cancellationToken),
                   FacilityMinPrice = await _courtRepository.GetMinPriceCourt(facility.Id, cancellationToken),
                   FacilityMaxPrice = await _courtRepository.GetMaxPriceCourt(facility.Id, cancellationToken),
                   FacilityDistance = 1.49209357181156954
               }
           );
            facilityCustomers.Add(
               new FacilityCustomerDTO
               {
                   FacilityID = facility.Id,
                   FacilityImage = facility.Image,
                   FacilityName = facility.Name,
                   FacilityAddress = facility.FullAddress,
                   FacilityRating = await _feedbackRepository.GetRatingFacility(facility.Id, cancellationToken),
                   FacilityMinPrice = await _courtRepository.GetMinPriceCourt(facility.Id, cancellationToken),
                   FacilityMaxPrice = await _courtRepository.GetMaxPriceCourt(facility.Id, cancellationToken),
                   FacilityDistance = 2.49209357181156954
               }
           );
        }

        if (!string.IsNullOrEmpty(request.Distance) &&
              double.TryParse(request.Distance, out double distance))
        {
            facilityCustomers = facilityCustomers.Where(x => x.FacilityDistance <= distance).ToList();
        }

        switch (request.OrderBy)
        {
            case "distanceAsc":
                facilityCustomers = facilityCustomers.OrderBy(x => x.FacilityDistance).ToList();
                break;

            case "distanceDesc":
                facilityCustomers = facilityCustomers.OrderByDescending(x => x.FacilityDistance).ToList();
                break;
        }

        facilityCustomers = facilityCustomers.Skip((request.RequestParams.CurrentPage - 1) * request.RequestParams.PageSize).Take(request.RequestParams.PageSize).ToList();

        var count = listFacility.Count - facilityCustomers.Count == 0 ? listFacility.Count - (listFacility.Count - facilityCustomers.Count) : listFacility.Count;
        return PagingResult<FacilityCustomerDTO>.Create(
               totalCount: count,
               pageSize: request.RequestParams.PageSize,
               currentPage: request.RequestParams.CurrentPage,
               totalPages: (int)Math.Ceiling(count / (double)request.RequestParams.PageSize),
               hasNext: request.RequestParams.CurrentPage > 1,
               hasPrevious: (int)Math.Ceiling(count / (double)request.RequestParams.PageSize) - request.RequestParams.CurrentPage > 0,
               results: facilityCustomers
           );

    }
    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        if (lat2 == 0 || lon2 == 0)
            return 0;

        var R = 6371;
        var dLat = (lat2 - lat1) * Math.PI / 180;
        var dLon = (lon2 - lon1) * Math.PI / 180;
        var a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distance = R * c;
        return distance;
    }

}
