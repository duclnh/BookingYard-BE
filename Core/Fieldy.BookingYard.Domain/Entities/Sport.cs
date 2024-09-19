using Fieldy.BookingYard.Domain.Common;

namespace Fieldy.BookingYard.Domain.Entities;

public class Sport : EntityBase<int>
{
    public required string SportName { get; set; }
    public string? Icon { get; set; }
    public string? Image{ get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public ICollection<Court>? Sports { get; set; }
}