using System.Linq.Expressions;
using AutoMapper;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Queries.GetAllUser
{
    public class GetALlUserQueryHandler : IRequestHandler<GetALlUserQuery, PagingResult<UserAdminDTO>>
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
        public async Task<PagingResult<UserAdminDTO>> Handle(GetALlUserQuery request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Domain.Entities.User, bool>>> expressions = new List<Expression<Func<Domain.Entities.User, bool>>>{
                x => x.Role != Role.Admin,
            };
            if (!string.IsNullOrEmpty(request.requestParams.Search))
            {
                expressions.Add(
                     x => x.Name.ToLower().Contains(request.requestParams.Search.ToLower().Trim())
                                || x.Phone.ToLower().Contains(request.requestParams.Search.ToLower().Trim())
                );
            }

            switch (request.OrderBy)
            {
                case "owner":
                    expressions.Add(
                         x => x.Role == Role.CourtOwner
                     );
                    break;

                case "customer":
                    expressions.Add(
                         x => x.Role == Role.Customer
                     );
                    break;

                case "active":
                    expressions.Add(
                         x => !x.IsDeleted);
                    break;

                case "deleted":
                    expressions.Add(
                     x => x.IsDeleted
                 );
                    break;
            }

            Expression<Func<Domain.Entities.User, bool>>[] expressionArray = expressions.ToArray();

            var list = await _userRepository.FindAllPaging(
                expressions: expressionArray,
                orderBy: x => x.OrderByDescending(o => o.CreatedAt),
                currentPage: request.requestParams.CurrentPage,
                pageSize: request.requestParams.PageSize,
                cancellationToken: cancellationToken
                );

            return PagingResult<UserAdminDTO>.Create(
                totalCount: list.TotalCount,
                pageSize: list.PageSize,
                currentPage: list.CurrentPage,
                totalPages: list.TotalPages,
                hasNext: list.HasNext,
                hasPrevious: list.HasPrevious,
                results: _mapper.Map<IList<UserAdminDTO>>(list.Results)
            );

        }
    }
}