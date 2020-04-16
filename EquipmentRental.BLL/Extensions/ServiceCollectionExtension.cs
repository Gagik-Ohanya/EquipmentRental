using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using EquipmentRental.BLL.AutoMapping;
using EquipmentRental.BLL.Facades;
using EquipmentRental.BLL.Facades.Interfaces;
using EquipmentRental.BLL.Services;
using EquipmentRental.BLL.Services.Interfaces;
using EquipmentRental.DAL.Repositories;
using EquipmentRental.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentRental.BLL.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IRentalTypeRepository, RentalTypeRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IRentalService, RentalService>();

            return services;
        }

        public static IServiceCollection AddFacades(this IServiceCollection services)
        {
            services.AddScoped<IEquipmentFacade, EquipmentFacade>();

            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
                config.AddExpressionMapping();
            });
            var mapper = mapperConfiguration.CreateMapper();
            
            return services.AddSingleton(mapper);
        }
    }
}