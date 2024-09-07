using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Application.Features.RegisterPackage.Commands.CreateRegisterPackage
{
	public class CreateRegisterPackageCommand : IRequest<string>
	{
		public Guid FacilityID { get; set; }
		public Guid PackageID { get; set; }
		public Guid UserID { get; set; }
		[Column("StartDate")]
		public DateTime RegisterDate { get; set; }
		[Column("EndDate")]
		public DateTime ExpiredDate { get; set; }
	}
}
