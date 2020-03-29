using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Configuration;
using ProConstructionsManagment.Desktop.Models;

namespace ProConstructionsManagment.Desktop.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IRequestProvider _requestProvider;

        public EmployeesService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Employee>> GetAllEmployees()
        {
            var uri = $"{Config.ApiUrlBase}/employees";

            var json = await _requestProvider.GetAsync<RootMultiple<Employee>>(uri);

            return json.Data;
        }

        public async Task<int> GetAllEmployeesCount()
        {
            var uri = $"{Config.ApiUrlBase}/employees";

            var json = await _requestProvider.GetAsync<RootMultiple<Employee>>(uri);

            return json.Summaries.Count;
        }

        public async Task<ObservableCollection<Employee>> GetAllHiredEmployees()
        {
            var uri = $"{Config.ApiUrlBase}/employees/hired";

            var json = await _requestProvider.GetAsync<RootMultiple<Employee>>(uri);

            return json.Data;
        }

        public async Task<ObservableCollection<Employee>> GetAllEmployeesForHire()
        {
            var uri = $"{Config.ApiUrlBase}/employees/hire";

            var json = await _requestProvider.GetAsync<RootMultiple<Employee>>(uri);

            return json.Data;
        }


        public async Task<Employee> GetEmployeeById(string employeeId)
        {
            var uri = $"{Config.ApiUrlBase}/employees/{employeeId}";

            var json = await _requestProvider.GetAsync<RootSingle<Employee>>(uri);

            return json.Data;
        }

        public async Task<ObservableCollection<Employee>> GetAllEmployeesByPositionAbleToRecruit(string positionId)
        {
            var uri = $"{Config.ApiUrlBase}/employees/recruitment/positions/{positionId}";

            var json = await _requestProvider.GetAsync<RootMultiple<Employee>>(uri);

            return json.Data;
        }

        public async Task<ObservableCollection<Employee>> GetAllEmployeesByProjectIdAndPositionId(string projectId, string positionId)
        {
            var uri = $"{Config.ApiUrlBase}/employees/recruitment/projects/{projectId}/positions/{positionId}";

            var json = await _requestProvider.GetAsync<RootMultiple<Employee>>(uri);

            return json.Data;
        }

        public RequestResult<Employee> AddEmployee(Employee model)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new ArgumentNullException(nameof(model.Name));

            if (string.IsNullOrWhiteSpace(model.LastName)) throw new ArgumentNullException(nameof(model.LastName));

            /*
            if (string.IsNullOrWhiteSpace(model.DateOfBirth))
                throw new ArgumentNullException(nameof(model.DateOfBirth));
                */

            if (string.IsNullOrWhiteSpace(model.Nationality))
                throw new ArgumentNullException(nameof(model.Nationality));

            try
            {
                var uri = $"{Config.ApiUrlBase}/employee/add";

                _requestProvider.PostAsync(uri, model);
            }
            catch
            {
                return new RequestResult<Employee>(false);
            }

            return new RequestResult<Employee>(true);
        }

        public RequestResult<Employee> UpdateEmployee(Employee model, string employeeId)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new ArgumentNullException(nameof(model.Name));

            if (string.IsNullOrWhiteSpace(model.LastName)) throw new ArgumentNullException(nameof(model.LastName));

            if (string.IsNullOrWhiteSpace(model.DateOfBirth))
                throw new ArgumentNullException(nameof(model.DateOfBirth));

            if (string.IsNullOrWhiteSpace(model.Nationality))
                throw new ArgumentNullException(nameof(model.Nationality));

            try
            {
                var uri = $"{Config.ApiUrlBase}/employees/{employeeId}/update";

                _requestProvider.PostAsync(uri, model);
            }
            catch
            {
                return new RequestResult<Employee>(false);
            }

            return new RequestResult<Employee>(true);
        }
    }
}