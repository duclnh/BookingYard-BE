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

            builder.HasData(
                new User
                {
                    Id = new Guid("57562e90-5835-4461-98f5-565cd760e6da"),
                    Name = "John Doe",
                    Address = "123 Main St, City, Country",
                    Email = "john@example.com",
                    Phone = "1234567890",
                    Gender = Gender.Other,
                    Point = 100,
                    PasswordHash = "$2a$10$qoOcRE7I52UUVlNSF3lrhOPhn6aLidL32gqEdeg7KezoOSDNjYQLa",
                    VerificationToken = null,
                    Role = Role.Customer,
                    IsBanned = false,
                    IsDeleted = false,
                    WardID = 07441,
                    CreatedAt = DateTime.Now,
                    CreatedBy = new Guid("57562e90-5835-4461-98f5-565cd760e6da"),
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = new Guid("57562e90-5835-4461-98f5-565cd760e6da")
                }
            );
            builder.HasData(
                new User
                {
                    Id = Guid.Parse("03b17c1c-0403-4e08-9ed6-738939dd911b"),
                    Name = "John Doe",
                    Address = "123 Main St, City, Country",
                    UserName = "lengochuynhduc7",
                    Email = "lengochuynhduc7@gmail.com",
                    Phone = "1234567890",
                    Gender = Gender.Other,
                    Point = 0,
                    PasswordHash = "$2a$10$qoOcRE7I52UUVlNSF3lrhOPhn6aLidL32gqEdeg7KezoOSDNjYQLa",
                    VerificationToken = null,
                    Role = Role.Admin,
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.Empty,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = Guid.Empty
                }
            );
            builder.HasData(
                new User
                {
                    Id = Guid.Parse("03b17c1c-0403-2e08-9ed3-709339dd911b"),
                    Name = "Nguyễn Minh Anh",
                    Address = "123 Main St, City, Country",
                    UserName = "lengochuynhduc2024",
                    Email = "lengochuynhduc2024@gmail.com",
                    Phone = "1234567890",
                    Gender = Gender.Other,
                    Point = 0,
                    PasswordHash = "$2a$10$qoOcRE7I52UUVlNSF3lrhOPhn6aLidL32gqEdeg7KezoOSDNjYQLa",
                    VerificationToken = null,
                    Role = Role.CourtOwner,
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.Parse("03b17c1c-0403-4e08-9ed6-738939dd911b"),
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = Guid.Parse("03b17c1c-0403-4e08-9ed6-738939dd911b")
                }
            );
			builder.HasData(
				new User
				{
					Id = Guid.Parse("03b17c1c-0403-2e08-9ed3-709339dd911c"),
					Name = "Lưu Gia Phong",
					Address = "123 Main St, City, Country",
					UserName = "lengochuynhduc2024",
					Email = "luuphong016@gmail.com",
					Phone = "1234567890",
					Gender = Gender.Other,
					Point = 0,
					PasswordHash = "$2a$10$qoOcRE7I52UUVlNSF3lrhOPhn6aLidL32gqEdeg7KezoOSDNjYQLa",
					VerificationToken = null,
					Role = Role.Customer,
					CreatedAt = DateTime.Now,
					CreatedBy = Guid.Parse("03b17c1c-0403-4e08-9ed6-738939dd911b"),
					ModifiedAt = DateTime.Now,
					ModifiedBy = Guid.Parse("03b17c1c-0403-4e08-9ed6-738939dd911b")
				}
			);
		}
        
    }
}