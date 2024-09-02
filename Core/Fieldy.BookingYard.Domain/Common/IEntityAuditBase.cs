using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Common{
    public interface IEntityAuditBase<TKey>: IEntityBase<TKey>, IAuditable {
    }
}