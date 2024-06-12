using Fieldy.BookingYard.Application.Common;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Domain.Common;
using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.DatabaseContext
{
    public class BookingYardDBContext : DbContext, IUnitOfWork
    {
        private readonly ICommonService _commonService;
        public BookingYardDBContext(DbContextOptions<BookingYardDBContext> options, ICommonService commonService) : base(options)
        {
            _commonService = commonService; 
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingYardDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = -1;
            using (var dbContextTransaction = base.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entry in base.ChangeTracker.Entries<IAuditEntity>()
                    .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
                    {
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.UpdateBy = entry.Entity.UpdateBy ?? _commonService.UserId ?? "";

                        if (entry.State == EntityState.Added)
                        {
                            entry.Entity.CreateDate = DateTime.Now;
                            entry.Entity.CreateBy = entry.Entity.UpdateBy ?? _commonService.UserId ?? "";
                        }
                    }
                    result = await base.SaveChangesAsync(cancellationToken);
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}