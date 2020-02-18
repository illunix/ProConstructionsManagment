using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Desktop.Configuration;
using GalaSoft.MvvmLight;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Views.Employees;

namespace ProConstructionsManagment.Desktop.Services
{
    public class EmployeesService : IEmployeesService
    {
        public readonly IRequestProvider _requestProvider;

        public EmployeesService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Employee>> GetAllEmployees()
        {
            var uri = $"{Config.ApiUrlBase}/employees";

            var json = await _requestProvider.GetAsync<Root<Employee>>(uri);

            return json.Data;
        }

        public async Task<int> GetAllEmployeesCount()
        {
            var uri = $"{Config.ApiUrlBase}/employees";

            var json = await _requestProvider.GetAsync<Root<Employee>>(uri);

            return json.Summaries.Count;
        }

        public async Task<ObservableCollection<Employee>> GetAllHiredEmployees()
        {
            var uri = $"{Config.ApiUrlBase}/employees/hired";

            var json = await _requestProvider.GetAsync<Root<Employee>>(uri);

            return json.Data;
        }

        public async Task<int> GetAllHiredEmployeesCount()
        {
            var uri = $"{Config.ApiUrlBase}/employees/hired";

            var json = await _requestProvider.GetAsync<Root<Employee>>(uri);

            return json.Summaries.Count;
        }

        public async Task<ObservableCollection<Employee>> GetAllEmployeesForHire()
        {
            var uri = $"{Config.ApiUrlBase}/employees/hire";

            var json = await _requestProvider.GetAsync<Root<Employee>>(uri);

            return json.Data;
        }

        public async Task<int> GetAllEmployeesForHireCount()
        {
            var uri = $"{Config.ApiUrlBase}/employees/hire";

            var json = await _requestProvider.GetAsync<Root<Employee>>(uri);

            return json.Summaries.Count;
        }

        public async Task<ObservableCollection<Employee>> GetEmployeeById(Guid employeeId)
        {
            var uri = $"{Config.ApiUrlBase}/employees/{employeeId}";

            var json = await _requestProvider.GetAsync<Root<Employee>>(uri);

            return json.Data;
        }

        public async Task<Employee> AddEmployee(Employee model)
        {
            var uri = $"{Config.ApiUrlBase}/employee/add";

            return await _requestProvider.PostAsync(uri, model);
        }

        public async Task<Employee> UpdateEmployee(Employee model, string employeeId)
        {
            var uri = $"{Config.ApiUrlBase}/employees/{employeeId}/update";

            return await _requestProvider.PostAsync(uri, model);
        }
    }
}