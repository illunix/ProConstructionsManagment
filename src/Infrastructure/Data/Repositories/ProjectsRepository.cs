using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProConstructionsManagment.Infrastructure.Data.Models;
using ProConstructionsManagment.Infrastructure.Enums;

namespace ProConstructionsManagment.Infrastructure.Data.Repositories
{
    public class ProjectsRepository : IBaseRepository<Project, ProjectStatus>
    {
        private readonly ProjectContext _context;

        public ProjectsRepository(ProjectContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Project>> GetAll()
        {
            return await _context.Projects
                .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<Project>> GetAllByStatus(ProjectStatus status)
        {
            return await _context.Projects
                .Where(project => project.Status == status)
                .ToArrayAsync();
        }

        public async Task<Project> GetById(Guid projectId)
        {
            return await _context.Projects
                .Where(project => project.Id == projectId)
                .FirstOrDefaultAsync();
        }

        public async Task<Project> Add(Project model)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            await _context.Projects
                .AddAsync(model, token);

            await _context.SaveChangesAsync(token);

            return model;
        }

        public async Task<Project> Update(Project model, Guid projectId)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            var project = await _context.Projects
                .FindAsync(projectId);

            project.Name = model.Name;

            _context.Projects
                .Update(project);

            await _context.SaveChangesAsync(token);

            return model;
        }
    }
}