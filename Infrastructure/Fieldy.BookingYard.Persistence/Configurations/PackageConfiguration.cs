﻿using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class PackageConfiguration : IEntityTypeConfiguration<Package>
	{
		public void Configure(EntityTypeBuilder<Package> builder)
		{
			builder.Property(x => x.Id).HasColumnName("PackageID");
			//set decimal data type
			builder.Property(p => p.PackagePrice)
			.HasColumnType("decimal(18,2)");

			builder.HasData(
				new Package
				{
					Id = Guid.Parse("57562e90-5835-4461-98f5-123cd760e612"),
					PackageName = "Package 1",
					PackagePrice = 100000,
					PackageDescription = "Package 1 Description",
					IsDeleted = false,
					CreatedAt = DateTime.Now,
					ModifiedAt = DateTime.Now
				},
				new Package
				{
					Id = Guid.Parse("57562e90-5835-4461-98f5-321cd7601234"),
					PackageName = "Package 2",
					PackagePrice = 200000,
					PackageDescription = "Package 2 Description",
					IsDeleted = false,
					CreatedAt = DateTime.Now,
					ModifiedAt = DateTime.Now
				}
				);
		}
	}
}
