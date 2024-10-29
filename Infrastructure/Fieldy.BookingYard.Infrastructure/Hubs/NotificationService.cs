using Fieldy.BookingYard.Application.Abstractions.Hub;
using Microsoft.AspNetCore.SignalR;

namespace Fieldy.BookingYard.Infrastructure.Hubs
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationCancelBooking(string userId, string message, string bookingId, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("CancelBooking", userId, message, bookingId, cancellationToken);
        }

        public async Task SendNotificationCreateBooking(string managerId, string message, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("CreateBooking", managerId, message, cancellationToken);
        }
    }
}