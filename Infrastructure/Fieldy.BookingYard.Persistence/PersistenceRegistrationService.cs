﻿using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Features.Package.Commands.CreatePackage;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Fieldy.BookingYard.Persistence.Repositories;
using FluentValidation;
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
            services.AddTransient<IPackageRepository, PackageRepository>();
            services.AddTransient<IRegisterPackageRepository, RegisterPackageRepository>();
            services.AddTransient<IDiscountRepository, DiscountRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			return services;
        }
    }

};

