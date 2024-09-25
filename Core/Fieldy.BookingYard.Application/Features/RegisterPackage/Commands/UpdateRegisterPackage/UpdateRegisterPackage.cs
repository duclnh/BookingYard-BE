using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Application.Features.RegisterPackage.Commands.UpdateRegisterPackage
{
	public class UpdateRegisterPackageCommand : IRequest<string>
	{
		public Guid RegisterPackageID { get; set; }
		public Guid FacilityID { get; set; }
		public Guid PackageID { get; set; }
		public Guid UserID { get; set; }
		[Column("StartDate")]
		public DateTime RegisterDate { get; set; }
		[Column("EndDate")]
		public DateTime ExpiredDate { get; set; }
	}
}
