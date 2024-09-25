using Fieldy.BookingYard.Domain.Abstractions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISupportRepository, SupportRepository>();
            services.AddTransient<IFacilityRepository, FacilityRepository>();
            services.AddTransient<IPackageRepository, PackageRepository>();
            services.AddTransient<IRegisterPackageRepository, RegisterPackageRepository>();
            services.AddTransient<IVoucherRepository, VoucherRepository>();
			services.AddTransient<IPeakHourRepository, PeakHourRepository>();
			services.AddTransient<IFacilityTimeRepository, FacilityTimeRepository>();
			services.AddTransient<IHolidayRepository, HolidayRepository>();
			services.AddTransient<IFeedbackRepository, FeedbackRepository>();
            services.AddTransient<IHistoryPointRepository, HistoryPointRepository>();
            services.AddTransient<ISportRepository, SportRepository>();
            services.AddTransient<ICourtRepository, CourtRepository>();
            services.AddTransient<ICollectVoucherRepository, CollectVoucherRepository>(); 
            services.AddTransient<IImageRepository, ImageRepository>();

			return services;
        }
    }

};

