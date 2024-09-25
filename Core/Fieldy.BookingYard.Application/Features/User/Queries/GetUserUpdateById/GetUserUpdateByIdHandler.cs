using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models.User;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Queries.GetUserUpdateById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserUpdateById, UserUpdateDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserUpdateDTO> Handle(GetUserUpdateById request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => x.Id == request.UserID, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.UserID);
           
            return _mapper.Map<UserUpdateDTO>(user);
        }
    }
}