using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Id)
                   .HasColumnName("EmployeeID");
            builder.HasKey(x => x.Id).HasName("EmployeeID");
        }
        
    }
}