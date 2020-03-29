
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProConstructionsManagment.Core.Entities;
using ProConstructionsManagment.Core.Enums;

namespace ProConstructionsManagment.Core.Interfaces
{
    public interface IEmployeesService
    {
        Task<IReadOnlyCollection<Employee>> GetAllEmployeesAbleToRecruit();
        Task<IReadOnlyCollection<Employee>> GetAllEmployeesByPosition(Guid positionId);
        Task<IReadOnlyCollection<Employee>> GetAllEmployeesByPositionAbleToRecruit(Guid positionId);
        Task<IReadOnlyCollection<Employee>> GetAllEmployeesByProjectIdAndPositionId(Guid projectId, Guid positionId);
    }
}
