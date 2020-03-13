using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProConstructionsManagment.Core.Entities;

namespace ProConstructionsManagment.Core.Interfaces
{
    public interface IProjectsRepository
    {
        Task<IEnumerable<ProjectCost>> GetAll();
        Task<IEnumerable<ProjectCost>> GetAllByProjectId(Guid projectId);
        Task<ProjectCost> GetById(Guid projectId);
    }
}
