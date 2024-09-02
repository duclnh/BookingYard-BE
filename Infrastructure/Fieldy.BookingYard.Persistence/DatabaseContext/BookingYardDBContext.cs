
using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Fieldy.BookingYard.Persistence.DatabaseContext
{
    public class BookingYardDBContext : DbContext
    {
        private readonly IJWTService _jwt;
        public BookingYardDBContext(DbContextOptions<BookingYardDBContext> options, IJWTService jwt) : base(options)
        {
            _jwt = jwt;
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingYardDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<IAuditable>()
               .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.ModifiedAt = DateTime.Now;
                entry.Entity.ModifiedBy = _jwt.AccountID;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = _jwt.AccountID;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}