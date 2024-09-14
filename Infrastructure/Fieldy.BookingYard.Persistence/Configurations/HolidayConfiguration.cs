using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fieldy.BookingYard.Persistence.Configurations
{
    public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.Property(x => x.Id).HasColumnName("HolidayID");
            builder.HasData(
                new Holiday
                {
                    Id = 1,
                    FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4"),
                    Date = new DateTime(DateTime.Now.Year, 9, 2),
                },
                new Holiday
                {
                    Id = 2,
                    FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4"),
                    Date = new DateTime(DateTime.Now.Year, 4, 30),
                },
                 new Holiday
                 {
                     Id = 3,
                     FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4"),
                     Date = new DateTime(DateTime.Now.Year, 5, 1),
                 }
            );
        }
    }
}
