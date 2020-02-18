using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ProConstructionsManagment.Infrastructure.Data.Entities;
using System.IO;

namespace ProConstructionsManagment.Infrastructure.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    public class ProjectContextFactory : IDesignTimeDbContextFactory<ProjectContext>
    {
        public ProjectContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ProjectContext>();
            var connectionString = "Server=51.77.140.201;Initial Catalog=ProConstructions;Persist Security Info=False;User ID=SA;Password=jkdf@HUD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
            builder.UseSqlServer(connectionString);
            return new ProjectContext(builder.Options);
        }
    }
}