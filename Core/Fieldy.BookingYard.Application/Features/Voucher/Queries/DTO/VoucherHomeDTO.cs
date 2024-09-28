using System;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.DTO;

public class VoucherHomeDTO
{
    public Guid VoucherID { get; set; }
    public required string VoucherName { get; set; }
    public required string Image { get; set; }
    public int Percentage { get; set; }
    public string? VoucherDescription { get; set; }
    public string? SportName { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime ExpiredDate { get; set; }
}
