using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProConstructionsManagment.Core.Entities;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;
using ProConstructionsManagment.Infrastructure.Data;
using ProConstructionsManagment.Infrastructure.Data.Repositories;
using ProConstructionsManagment.Infrastructure.Errors;
using ProConstructionsManagment.Infrastructure.Repositories;

namespace ProConstructionsManagment.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionString"]));

            services.AddScoped<IAsyncRepository<Project, ProjectStatus>, AsyncRepository<Project, ProjectStatus>>();
            services.AddScoped<IAsyncRepository<ProjectCost>, AsyncRepository<ProjectCost>>();
            services.AddScoped<IProjectsRepository, ProjectsRepository>();

            services.AddScoped<IAsyncRepository<Employee, EmployeeStatus>, AsyncRepository<Employee, EmployeeStatus>>();

            services.AddScoped<IAsyncRepository<Client>, AsyncRepository<Client>>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}