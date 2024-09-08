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
		}
	}
}
