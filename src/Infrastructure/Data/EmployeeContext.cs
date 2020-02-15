using Microsoft.EntityFrameworkCore;
using ProConstructionsManagment.Infrastructure.Data.Models;

namespace ProConstructionsManagment.Infrastructure.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}