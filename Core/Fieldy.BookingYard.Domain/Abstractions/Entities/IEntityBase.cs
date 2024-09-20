namespace Fieldy.BookingYard.Domain.Abstractions.Entities{
    public interface IEntityBase<TKey> {
        TKey Id { get; }
    }
}