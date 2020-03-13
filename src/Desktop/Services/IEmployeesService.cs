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

        Task<ObservableCollection<Employee>> GetAllEmployeesForHire();

        Task<Employee> GetEmployeeById(string employeeId);

        RequestResult<Employee> AddEmployee(Employee model);

        RequestResult<Employee> UpdateEmployee(Employee model, string employeeId);
    }
}