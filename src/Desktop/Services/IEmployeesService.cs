using ProConstructionsManagment.Desktop.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Services
{
    public interface IEmployeesService
    {
        Task<ObservableCollection<Employee>> GetAllEmployees();

        Task<int> GetAllEmployeesCount();

        Task<ObservableCollection<Employee>> GetAllHiredEmployees();

        Task<ObservableCollection<Employee>> GetAllEmployeesForHire();

        Task<Employee> GetEmployeeById(string employeeId);

        Task<ObservableCollection<Employee>> GetAllEmployeesByPositionAbleToRecruit(string positionId);

        Task<ObservableCollection<Employee>> GetAllEmployeesByProjectIdAndPositionId(string projectId, string positionId);

        RequestResult<Employee> AddEmployee(Employee model);

        RequestResult<Employee> UpdateEmployee(Employee model, string employeeId);
    }
}