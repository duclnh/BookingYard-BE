using Fieldy.BookingYard.Application.Models.Statistic;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Support.Queries.GetStatisticSupport{
    public record GetStatisticSupportQuery : IRequest<StatisticSupportDTO>{}
}