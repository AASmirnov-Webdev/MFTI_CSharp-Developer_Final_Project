using Domain.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
            services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
            services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();

            return services;
        }
    }
}
