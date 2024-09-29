using System;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.DTO;

public class VoucherHomeDTO
{
    public Guid VoucherID { get; set; }
    public required string VoucherName { get; set; }
    public string? FacilityName { get; set; }
    public int Percentage { get; set; }
    public string? SportName { get; set; }
}
