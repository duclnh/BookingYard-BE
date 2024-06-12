using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id)
                   .HasColumnName("UserID");
            builder.HasKey(x => x.Id).HasName("UserID");
            var newUserId = Guid.NewGuid().ToString("N");
            builder.HasData(
                new User
                {
                    Id = newUserId,
                    Name = "John Doe",
                    Address = "123 Main St, City, Country",
                    Email = "john@example.com",
                    Phone = "1234567890",
                    Gender = true,
                    Point = 100,
                    PasswordHash = "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC",
                    VerificationToken= "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC",
                    Role = Role.Customer,
                    CreateDate = DateTime.Now,
                    CreateBy = newUserId,
                    UpdateDate = DateTime.Now,
                    UpdateBy = newUserId
                }
            );
        }
        
    }
}