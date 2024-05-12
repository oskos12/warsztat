using Microsoft.EntityFrameworkCore;
using Warsztat.Data.Entity;

namespace Warsztat.Data
{
    public interface IWorkshopDbContext
    {
        DbContext Context { get; }
        DbSet<Cars> CarsSet { get; set; }
        DbSet<Dictionary> DictionarySet { get; set; }
        DbSet<Clients> ClientsSet { get; set; }
        DbSet<Users> UsersSet { get; set; }
        DbSet<Applications> ApplicationsSet { get; set; }
        DbSet<ApplicationsView> ApplicationsViewSet { get; set; }
        DbSet<Workers> WorkersSet { get; set; }
        DbSet<Services> ServicesSet { get; set; }
        DbSet<WorkersServices> WorkersServicesSet { get; set; }
        DbSet<ServicesView> ServicesViewSet { get; set; }
    }
}