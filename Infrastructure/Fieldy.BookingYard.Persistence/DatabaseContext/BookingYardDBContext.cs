
using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Fieldy.BookingYard.Persistence.DatabaseContext
{
    public class BookingYardDBContext : DbContext, IUnitOfWork
    {
        private readonly IJWTService _jwt;
        public BookingYardDBContext(DbContextOptions<BookingYardDBContext> options, IJWTService jwt) : base(options)
        {
            _jwt = jwt;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<RegisterPackage> RegisterPackages { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<PeakHour> PeakHours { get; set; }
        public DbSet<FacilityTime> FacilityTimes { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<FeedBack> Feedbacks { get; set; }
        public DbSet<Image> Images{ get; set; }
        public DbSet<HistoryPoint> HistoryPoints { get; set; }
        
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
                entry.Entity.ModifiedBy = _jwt.UserID;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = _jwt.UserID;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}