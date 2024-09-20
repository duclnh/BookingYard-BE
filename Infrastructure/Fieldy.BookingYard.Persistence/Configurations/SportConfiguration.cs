using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations;

public class SportConfiguration : IEntityTypeConfiguration<Sport>
{
    public void Configure(EntityTypeBuilder<Sport> builder)
    {
        builder.Property(x => x.Id).HasColumnName("SportID");
        builder.HasData(
            new Sport
            {
                Id = 1,
                SportName = "Bóng đá",
                CreateAt = DateTime.Now,
                IsDeleted = false,
            },
             new Sport
             {
                 Id = 2,
                 SportName = "Pickle Ball",
                 CreateAt = DateTime.Now,
                 IsDeleted = false,
             }
        );
    }
}
