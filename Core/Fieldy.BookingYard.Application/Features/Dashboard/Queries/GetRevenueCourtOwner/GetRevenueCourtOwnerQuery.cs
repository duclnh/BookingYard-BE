using Fieldy.BookingYard.Application.Features.Dashboard.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Dashboard.Queries.GetRevenueCourtOwner
{
	public class GetRevenueCourtOwnerQuery : IRequest<DashboardCourtOwner>
	{
		public string? TypeTimeBased { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public Guid FacilityId { get; set; }
	}
}
