using ProConstructionsManagment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Core.Interfaces
{
    public interface IProjectsRepository
    {
        Task<IEnumerable<ProjectCost>> GetAllProjectCosts(Guid projectId);

        Task<ProjectCost> GetProjectCost(Guid projectCostId);

        Task<IEnumerable<ProjectRecruitment>> GetProjectRecruitments(Guid projectId);

        Task<ProjectRecruitment> GetProjectRecruitment(Guid projectRecruitmentId);
    }
}