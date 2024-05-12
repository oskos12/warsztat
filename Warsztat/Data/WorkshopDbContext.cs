using Microsoft.EntityFrameworkCore;
using Warsztat.Data.Entity;

namespace Warsztat.Data
{
    public class WorkshopDbContext : DbContext, IWorkshopDbContext
    {
        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options) : base(options)
        {
            Context = this;
        }
        public DbContext Context { get; }
        public DbSet<Cars> CarsSet { get; set; }
        public DbSet<Dictionary> DictionarySet { get; set; }
        public DbSet<Clients> ClientsSet { get; set; }
        public DbSet<Users> UsersSet { get; set; }
        public DbSet<Applications> ApplicationsSet { get; set; }
        public DbSet<ApplicationsView> ApplicationsViewSet { get; set; }
        public DbSet<Workers> WorkersSet { get; set; }
        public DbSet<Services> ServicesSet { get; set; }
        public DbSet<WorkersServices> WorkersServicesSet { get; set; }
        public DbSet<ServicesView> ServicesViewSet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cars>().ToTable("CarsSet", "dbo");
            builder.Entity<Dictionary>().ToTable("DictionarySet", "dbo");
            builder.Entity<Clients>().ToTable("ClientsSet", "dbo");
            builder.Entity<Users>().ToTable("UsersSet", "dbo");
            builder.Entity<Applications>().ToTable("ApplicationsSet", "dbo");
            builder.Entity<ApplicationsView>().ToTable("vApplications", "dbo");
            builder.Entity<Workers>().ToTable("WorkersSet", "dbo");
            builder.Entity<Services>().ToTable("ServicesSet", "dbo");
            builder.Entity<WorkersServices>().ToTable("WorkersServicesSet", "dbo");
            builder.Entity<ServicesView>().ToTable("vServices", "dbo");
        }
    }
}
