using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Warsztat.Contracts;

namespace Warsztat.Infrastructure.Extenstions
{
    public static class ServiceRegistrationExtenstion
    {
        public static void AddServicesInAssemlby(this IServiceCollection services, IConfiguration configuration)
        {
            foreach (var appService in typeof(Startup).Assembly
                     .DefinedTypes
                     .Where(x => typeof(IServiceRegistration)
                         .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                     .Select(Activator.CreateInstance)
                     .Cast<IServiceRegistration>())
            {
                appService.RegisterAppService(services, configuration);
            }
        }
    }
}
