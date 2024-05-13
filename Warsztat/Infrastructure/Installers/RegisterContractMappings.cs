using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warsztat.Contracts;
using Warsztat.Data;
using Warsztat.Data.DataManager;

namespace Warsztat.Infrastructure.Installers
{
    internal class RegisterContractMappings : IServiceRegistration
    {
        public void RegisterAppService(IServiceCollection services, IConfiguration configuration)
        {
            //Register Interface Mappings for Repositories
            services.AddTransient<ISqlServerManager, SqlServerManager>();
            services.AddTransient<IWorkshopManager, WorkshopManager>();

            services.AddScoped<IWorkshopDbContext>(service => service.GetService<WorkshopDbContext>());
        }
    }
}
