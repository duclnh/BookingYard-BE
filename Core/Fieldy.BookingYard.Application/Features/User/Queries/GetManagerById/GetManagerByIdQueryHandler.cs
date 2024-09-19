using System.Linq.Expressions;
using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.User.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Queries.GetManagerById
{
    public class GetManagerByIdQueryHandler : IRequestHandler<GetManagerByIdQuery, ManagerDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMapper _mapper;

        public GetManagerByIdQueryHandler(IUserRepository userRepository, IFacilityRepository facilityRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _facilityRepository = facilityRepository;
            _mapper = mapper;
        }

        public async Task<ManagerDTO> Handle(GetManagerByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => x.Id == request.UserID, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.UserID);

            var facility = await _facilityRepository.Find(x => x.UserID == user.Id, cancellationToken);

            var response = _mapper.Map<ManagerDTO>(user);
            if(facility != null)
            {
                response.FacilityName = facility.Name;
                response.FacilityID = facility.Id;
                response.FacilityImage = facility.Logo ?? facility.Image;
            }

            return response;
        }
    }
}