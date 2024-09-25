using Fieldy.BookingYard.Application.Models.Statistic;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Support.Queries.GetStatisticSupport
{
    public class GetStatisticSupportQueryHandler : IRequestHandler<GetStatisticSupportQuery, StatisticSupportDTO>
    {
        private readonly ISupportRepository _supportRepository;

        public GetStatisticSupportQueryHandler(ISupportRepository supportRepository)
        {
            _supportRepository = supportRepository;
        }

        public async Task<StatisticSupportDTO> Handle(GetStatisticSupportQuery request, CancellationToken cancellationToken)
        {

            return new StatisticSupportDTO(
                totalSupport: await _supportRepository.CountAsync(filterExpression: x => true, cancellationToken: cancellationToken),
                totalProcessed: await _supportRepository.CountAsync(filterExpression: x => x.ModifiedBy != null, cancellationToken: cancellationToken)
            );
        }
    }
}