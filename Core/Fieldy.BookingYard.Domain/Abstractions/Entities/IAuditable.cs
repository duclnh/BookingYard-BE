namespace Fieldy.BookingYard.Domain.Abstractions.Entities
{
    public interface IAuditable : IDateTracking, IUserTracking, ISoftDelete
    {
    }

}