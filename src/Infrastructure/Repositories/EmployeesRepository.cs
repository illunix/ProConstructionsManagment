using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProConstructionsManagment.Core.Entities;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;
using ProConstructionsManagment.Infrastructure.Data;

namespace ProConstructionsManagment.Infrastructure.Repositories
{
    public class EmployeesRepository : IEmployeesService
    {
        private readonly ApplicationDbContext _context;

        public EmployeesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Employee>> GetAllEmployeesAbleToRecruit()
        {
            return await _context.Employees
                .Where(employee => employee.ProjectId == Guid.Empty)
                .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<Employee>> GetAllEmployeesByPosition(Guid positionId)
        {
            return await _context.Employees
                .Where(employee => employee.PositionId == positionId)
                .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<Employee>> GetAllEmployeesByPositionAbleToRecruit(Guid positionId)
        {
            return await _context.Employees
                .Where(employee => employee.ProjectId == Guid.Empty && employee.PositionId == positionId)
                .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<Employee>> GetAllEmployeesByProjectIdAndPositionId(Guid projectId, Guid positionId)
        {
            return await _context.Employees
                .Where(employee => employee.ProjectId == projectId && employee.PositionId == positionId)
                .ToArrayAsync();
        }
    }
}
