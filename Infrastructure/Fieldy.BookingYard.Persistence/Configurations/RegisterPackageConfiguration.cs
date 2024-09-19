using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class RegisterPackageConfiguration : IEntityTypeConfiguration<RegisterPackage>
	{
		public void Configure(EntityTypeBuilder<RegisterPackage> builder)
		{
			builder.Property(x => x.Id).HasColumnName("RegisterPackageID");
			/*builder.Property(x => x.RegisterDate).HasColumnName("StartDate");
			builder.Property(x => x.ExpiredDate).HasColumnName("EndDate");*/
			builder.HasOne(x => x.Facility).WithMany(f => f.RegisterPackages).HasForeignKey(x => x.FacilityID);
			builder.HasOne(x => x.Package).WithMany(p => p.RegisterPackages).HasForeignKey(x => x.PackageID);

		}
	}
}
