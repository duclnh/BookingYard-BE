using System;
using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Features.Sport.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Sport.Queries.GetSportCreate;

public class GetSportCreateQueryHandler : IRequestHandler<GetSportCreateQuery, IList<SportCreateDTO>>
{
    private readonly ISportRepository _sportRepository;
    private readonly IMapper _mapper;

    public GetSportCreateQueryHandler(ISportRepository sportRepository, IMapper mapper)
    {
        _sportRepository = sportRepository;
        _mapper = mapper;
    }

    public async Task<IList<SportCreateDTO>> Handle(GetSportCreateQuery request, CancellationToken cancellationToken)
    {
        var sports = await _sportRepository.FindAll(
            expression: x => x.IsDeleted == false,
            cancellationToken: cancellationToken
        );

        return _mapper.Map<IList<SportCreateDTO>>(sports);
    }
}
