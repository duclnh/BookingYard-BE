using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.Property(x => x.Id)
               .HasColumnName("ImageID");
        builder.HasData(
           new Image
           {
               Id = 1,
               ImageLink = "/facility/bcc982ee-e942-43f9-8ab5-6fad3740135e.jpg",
               FacilityID = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4"),
           }
        );
    }
}
