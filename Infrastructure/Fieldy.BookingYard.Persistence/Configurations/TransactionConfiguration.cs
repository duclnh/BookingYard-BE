using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
	{
		public void Configure(EntityTypeBuilder<Transaction> builder)
		{

			builder.Property(x => x.Id).HasColumnName("TransactionID");
		}
	}
}
