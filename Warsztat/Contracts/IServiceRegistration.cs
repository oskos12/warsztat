using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Warsztat.Contracts
{
    public interface IServiceRegistration
    {
        void RegisterAppService(IServiceCollection services, IConfiguration configuration);
    }
}
