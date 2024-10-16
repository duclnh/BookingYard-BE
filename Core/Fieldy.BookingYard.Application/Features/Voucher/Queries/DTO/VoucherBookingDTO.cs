using System;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.DTO;

public class VoucherBookingDTO
{
    public Guid VoucherID { get; set; }
    public string? VoucherName { get; set; }
    public int Percentage { get; set; }
    public string? Image { get; set; }
    public string? SportName { get; set; }
    public string? FacilityName { get; set; }
    public required string RegisterDate { get; set; }
    public required string ExpiredDate { get; set; }
}
