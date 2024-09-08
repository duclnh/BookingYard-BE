using MediatR;

namespace Fieldy.BookingYard.Application.Features.Discount.Command.CreateDiscount
{
	public class CreateDiscountCommand : IRequest<string>
	{
		public string? DiscountName { get; set; }
		public string? Image { get; set; }
		public int Percentage { get; set; }
		public string? DiscountDescription { get; set; }
		public DateTime RegisterDate { get; set; }
		public DateTime ExpiredDate { get; set; }
		public string? Reason { get; set; }
		public bool Status { get; set; }
		public Guid FacilityID { get; set; }
		public Guid CategorySportID { get; set; }
	}
}
