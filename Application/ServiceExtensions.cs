using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;

namespace Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<AnimalTransferService>();
            services.AddScoped<FeedingOrganizationService>();
            services.AddScoped<ZooStatisticsService>();

            return services;
        }
    }
}
