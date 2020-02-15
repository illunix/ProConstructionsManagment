using ProConstructionsManagment.Infrastructure.Data.Models;
using ProConstructionsManagment.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Infrastructure.Data.Repositories
{
    public interface IEmployeesRepository
    {
        Task<IReadOnlyCollection<Employee>> GetAll();

        Task<IReadOnlyCollection<Employee>> GetAllByStatus(EmployeeStatus status);

        Task<Employee> GetById(Guid employeeId);

        Task<Employee> Add(Employee model);

        Task<Employee> Update(Employee model, Guid employeeId);
    }
}