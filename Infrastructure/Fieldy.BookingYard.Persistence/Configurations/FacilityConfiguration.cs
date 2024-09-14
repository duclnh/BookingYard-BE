using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations;

public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
{
       public void Configure(EntityTypeBuilder<Facility> builder)
       {
              builder.Property(x => x.Id)
                     .HasColumnName("FacilityID");
              builder.Property(x => x.Name)
                     .HasColumnName("FacilityName");
              builder.Property(x => x.MonthPrice)
                     .HasColumnType("decimal(18,2)");
              builder.Property(x => x.YearPrice)
                     .HasColumnType("decimal(18,2)");
              builder.Property(x => x.PeakHourPrice)
                     .HasColumnType("decimal(18,2)");
              builder.Property(x => x.HolidayPrice)
                     .HasColumnType("decimal(18,2)");
    }
}