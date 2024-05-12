using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warsztat.Contracts;
using Warsztat.Data;

namespace Warsztat.Infrastructure.Installers
{
    public class RegisterDbContext : IServiceRegistration
    {
        public void RegisterAppService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkshopDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
