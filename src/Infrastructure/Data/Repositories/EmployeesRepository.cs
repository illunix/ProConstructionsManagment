using Microsoft.EntityFrameworkCore;
using ProConstructionsManagment.Infrastructure.Data.Models;
using ProConstructionsManagment.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Infrastructure.Data.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EmployeeContext _context;

        public EmployeesRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Employee>> GetAll()
        {
            return await _context.Employees
                .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<Employee>> GetAllByStatus(EmployeeStatus status)
        {
            return await _context.Employees
                .Where(employee => employee.Status == status)
                .ToArrayAsync();
        }

        public async Task<Employee> GetById(Guid employeeId)
        {
            return await _context.Employees
                .Where(employee => employee.Id == employeeId)
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> Add(Employee model)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            await _context.Employees
                .AddAsync(model, token);

            await _context.SaveChangesAsync(token);

            return model;
        }

        public async Task<Employee> Update(Employee model, Guid employeeId)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            var employee = await _context.Employees
                .FindAsync(employeeId);

            employee.Name = model.Name;

            _context.Employees
                .Update(employee);

            await _context.SaveChangesAsync(token);

            return model;
        }
    }
}