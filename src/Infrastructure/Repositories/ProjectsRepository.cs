using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProConstructionsManagment.Core.Entities;
using ProConstructionsManagment.Core.Interfaces;
using ProConstructionsManagment.Infrastructure.Data;

namespace ProConstructionsManagment.Infrastructure.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectCost>> GetAll()
        {
            return await _context.ProjectCosts
                .ToArrayAsync();
        }

        public async Task<IEnumerable<ProjectCost>> GetAllByProjectId(Guid projectId)
        {
            return await _context.ProjectCosts
                .Where(projectCost => projectCost.ProjectId == projectId)
                .ToArrayAsync();
        }

        public async Task<ProjectCost> GetById(Guid projectCostId)
        {
            return await _context.ProjectCosts
                .Where(projectCost => projectCost.Id == projectCostId)
                .FirstOrDefaultAsync();
        }
    }
}
