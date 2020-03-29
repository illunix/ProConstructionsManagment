using Microsoft.EntityFrameworkCore;
using ProConstructionsManagment.Core.Entities;
using ProConstructionsManagment.Core.Interfaces;
using ProConstructionsManagment.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Infrastructure.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectCost>> GetAllProjectCosts(Guid projectId)
        {
            return await _context.ProjectCosts
                .Where(projectCost => projectCost.ProjectId == projectId)
                .ToArrayAsync();
        }

        public async Task<ProjectCost> GetProjectCost(Guid projectCostId)
        {
            return await _context.ProjectCosts
                .Where(projectCost => projectCost.Id == projectCostId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProjectRecruitment>> GetProjectRecruitments(Guid projectId)
        {
            return await _context.ProjectsRecruitments
                .Where(projectRecruitment => projectRecruitment.ProjectId == projectId)
                .ToArrayAsync();
        }

        public async Task<ProjectRecruitment> GetProjectRecruitment(Guid projectRecruitmentId)
        {
            return await _context.ProjectsRecruitments
                .Where(projectRecruitment => projectRecruitment.Id == projectRecruitmentId)
                .FirstOrDefaultAsync();
        }
    }
}