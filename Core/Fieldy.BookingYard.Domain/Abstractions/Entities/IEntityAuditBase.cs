using Fieldy.BookingYard.Domain.Abstractions.Entities;

namespace Fieldy.BookingYard.Domain.Commons{
    public interface IEntityAuditBase<TKey>: IEntityBase<TKey>, IAuditable {
    }
}