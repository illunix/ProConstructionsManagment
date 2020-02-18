using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Models;

namespace ProConstructionsManagment.Desktop.Services
{
    public interface IEmployeesService
    {
        Task<ObservableCollection<Employee>> GetAllEmployees();

        Task<int> GetAllEmployeesCount();

        Task<ObservableCollection<Employee>> GetAllHiredEmployees();

        Task<int> GetAllHiredEmployeesCount();

        Task<ObservableCollection<Employee>> GetAllEmployeesForHire();

        Task<int> GetAllEmployeesForHireCount();

        Task<ObservableCollection<Employee>> GetEmployeeById(Guid employeeId);

        Task<Employee> AddEmployee(Employee model);

        Task<Employee> UpdateEmployee(Employee model, string employeeId);
    }
}