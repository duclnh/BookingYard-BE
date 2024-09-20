using Fieldy.BookingYard.Domain.Abstractions.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions
{
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        public required TKey Id {get; set;}
    }
}