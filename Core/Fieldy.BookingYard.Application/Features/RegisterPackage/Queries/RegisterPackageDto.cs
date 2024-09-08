using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Application.Features.RegisterPackage.Queries
{
	public class RegisterPackageDto
    {
		public Guid RegisterPackageID { get; set; }
		public Guid FacilityID { get; set; }
		public Guid PackageID { get; set; }
		public Guid UserID { get; set; }
		public DateTime RegisterDate { get; set; }
		public DateTime ExpiredDate { get; set; }
	}
}
