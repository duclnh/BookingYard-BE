using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Fieldy.BookingYard.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fieldy.BookingYard.Persistence
{
    public static class PersistenceRegistrationService
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingYardDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectString"));
            });

            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

            return services;
        }
    }

};

