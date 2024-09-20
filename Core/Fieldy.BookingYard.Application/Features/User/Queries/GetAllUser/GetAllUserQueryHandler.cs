using AutoMapper;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Queries.GetAllUser
{
    public class GetALlUserQueryHandler : IRequestHandler<GetALlUserQuery, PagingResult<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetALlUserQueryHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<PagingResult<UserDTO>> Handle(GetALlUserQuery request, CancellationToken cancellationToken)
        {


            var list = await _userRepository.FindAllPaging(
                expression: x => x.Name.ToLower().Contains(request.requestParams.Search.ToLower().Trim())
                                || x.Email.ToLower().Contains(request.requestParams.Search.ToLower().Trim()),
                orderBy: x => x.OrderByDescending(o => o.CreatedAt),
                currentPage: request.requestParams.CurrentPage,
                pageSize: request.requestParams.PageSize,
                cancellationToken: cancellationToken
                );

            return PagingResult<UserDTO>.Create(
                totalCount: list.TotalCount,
                pageSize: list.PageSize,
                currentPage: list.CurrentPage,
                totalPages: list.TotalPages,
                hasNext: list.HasNext,
                hasPrevious: list.HasPrevious,
                results: _mapper.Map<IList<UserDTO>>(list.Results)
            );

        }
    }
}