namespace Fieldy.BookingYard.Domain.Abstractions.Entities
{
    public interface IDateTracking
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}