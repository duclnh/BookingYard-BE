using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
    public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.Property(x => x.Id)
                   .HasColumnName("FacilityID");
            builder.HasKey(x => x.Id).HasName("UserID");
            var newUserId = Guid.NewGuid().ToString("N");
        }
        
    }
}