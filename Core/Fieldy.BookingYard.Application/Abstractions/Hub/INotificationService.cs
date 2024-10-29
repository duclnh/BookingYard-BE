namespace Fieldy.BookingYard.Application.Abstractions.Hub
{
    public interface INotificationService
    {
        Task SendNotificationCreateBooking(string managerId, string message, CancellationToken cancellationToken);
        Task SendNotificationCancelBooking(string userId, string message, string bookingId, CancellationToken cancellationToken);
    }
}