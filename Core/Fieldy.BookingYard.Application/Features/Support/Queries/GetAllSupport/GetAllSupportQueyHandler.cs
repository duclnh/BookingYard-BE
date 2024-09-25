using AutoMapper;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Support.Queries.GetAllSupport
{
    public class GetAllSupportQueyHandler : IRequestHandler<GetAllSupportQuery, PagingResult<SupportDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ISupportRepository _supportRepository;

        public GetAllSupportQueyHandler(IMapper mapper, ISupportRepository supportRepository)
        {
            _mapper = mapper;
            _supportRepository = supportRepository;
        }

        public async Task<PagingResult<SupportDTO>> Handle(GetAllSupportQuery request, CancellationToken cancellationToken)
        {
            var listSupport = await _supportRepository.FindAllPaging(
                currentPage: request.requestParams.CurrentPage,
                pageSize: request.requestParams.PageSize,
                expression: x => (string.IsNullOrEmpty(request.requestParams.Search) || (x.Name.ToLower().Contains(request.requestParams.Search.ToLower().Trim())
                                || x.Phone.Contains(request.requestParams.Search.Trim())))
                                && (!request.typeSupport.HasValue || x.TypeSupport == request.typeSupport),
                orderBy: x => x.OrderByDescending(x => x.CreatedAt),
                cancellationToken: cancellationToken
            );


            return PagingResult<SupportDTO>.Create(
               totalCount: listSupport.TotalCount,
               pageSize: listSupport.PageSize,
               currentPage: listSupport.CurrentPage,
               totalPages: listSupport.TotalPages,
               hasNext: listSupport.HasNext,
               hasPrevious: listSupport.HasPrevious,
               results: _mapper.Map<IList<SupportDTO>>(listSupport.Results)
           );
        }
    }
}
