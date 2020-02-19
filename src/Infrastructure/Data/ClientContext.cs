using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProConstructionsManagment.Infrastructure.Data.Entities;

namespace ProConstructionsManagment.Infrastructure.Data
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    public class ClientContextFactory : IDesignTimeDbContextFactory<ClientContext>
    {
        public ClientContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ClientContext>();
            var connectionString = "Server=51.77.140.201;Initial Catalog=ProConstructions;Persist Security Info=False;User ID=SA;Password=jkdf@HUD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
            builder.UseSqlServer(connectionString);
            return new ClientContext(builder.Options);
        }
    }
}