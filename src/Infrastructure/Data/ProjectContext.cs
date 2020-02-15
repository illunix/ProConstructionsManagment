using Microsoft.EntityFrameworkCore;
using ProConstructionsManagment.Infrastructure.Data.Models;

namespace ProConstructionsManagment.Infrastructure.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
    }
}