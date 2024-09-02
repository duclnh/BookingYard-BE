namespace Fieldy.BookingYard.Domain.Common{
    public interface IEntityBase<TKey> {
        TKey Id { get; }
    }
}