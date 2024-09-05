using System;
using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Paging;
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
                requestParams: request.requestParams,
                expression: x => x.Name.ToLower().Contains(request.requestParams.Search.ToLower().Trim())
                                || x.Phone.Contains(request.requestParams.Search.Trim()),
                orderBy: x => x.OrderByDescending(x => x.CreatedAt),
                cancellationToken: cancellationToken);

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
