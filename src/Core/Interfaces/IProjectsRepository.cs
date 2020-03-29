using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProConstructionsManagment.Core.Entities;

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
