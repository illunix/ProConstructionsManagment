using Microsoft.EntityFrameworkCore;
using ProConstructionsManagment.Core.Entities;
using ProConstructionsManagment.Core.Interfaces;

namespace ProConstructionsManagment.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectCost> ProjectCosts { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}