using Fieldy.BookingYard.Domain.Common;

namespace Fieldy.BookingYard.Domain.Entities
{
    public interface IAuditable : IDateTracking, IUserTracking, ISoftDelete
    {
    }

}