using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.Id)
                   .HasColumnName("CustomerID");
            builder.HasKey(x => x.Id).HasName("UserID");
            var newUserId = Guid.NewGuid().ToString("N");
        }
        
    }
}