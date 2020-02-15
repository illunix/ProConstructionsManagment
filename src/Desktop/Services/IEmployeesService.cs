using ProConstructionsManagment.Desktop.DTO;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Services
{
    public interface IEmployeesService
    {
        Task<ObservableCollection<Datum>> GetAllEmployees();

        Task<int> GetAllEmployeesCount();

        Task<ObservableCollection<Datum>> GetAllHiredEmployees();

        Task<int> GetAllHiredEmployeesCount();

        Task<ObservableCollection<Datum>> GetAllEmployeesForHire();

        Task<int> GetAllEmployeesForHireCount();

        Task<Datum> GetEmployeeById(Guid employeeId);

        Task<Datum> AddEmployee(Datum dto);

        Task<Datum> UpdateEmployee(Datum dto, string employeeId);
    }
}