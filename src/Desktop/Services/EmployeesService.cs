using ProConstructionsManagment.Desktop.DTO;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Services
{
    public class EmployeesService : IEmployeesService
    {
        private const string ApiUrlBase = "https://a2513563.ngrok.io/api/v1";

        public readonly IRequestProvider _requestProvider;

        public EmployeesService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Datum>> GetAllEmployees()
        {
            var uri = $"{ApiUrlBase}/employees";

            var json = await _requestProvider.GetAsync<Root>(uri);

            return json.data;
        }

        public async Task<int> GetAllEmployeesCount()
        {
            var uri = $"{ApiUrlBase}/employees";

            var json = await _requestProvider.GetAsync<Root>(uri);

            return json.Summaries.Count;
        }

        public async Task<ObservableCollection<Datum>> GetAllHiredEmployees()
        {
            var uri = $"{ApiUrlBase}/employees/hired";

            var json = await _requestProvider.GetAsync<Root>(uri);

            return json.data;
        }

        public async Task<int> GetAllHiredEmployeesCount()
        {
            var uri = $"{ApiUrlBase}/employees/hired";

            var json = await _requestProvider.GetAsync<Root>(uri);

            return json.Summaries.Count;
        }

        public async Task<ObservableCollection<Datum>> GetAllEmployeesForHire()
        {
            var uri = $"{ApiUrlBase}/employees/hire";

            var json = await _requestProvider.GetAsync<Root>(uri);

            return json.data;
        }

        public async Task<int> GetAllEmployeesForHireCount()
        {
            var uri = $"{ApiUrlBase}/employees/hire";

            var json = await _requestProvider.GetAsync<Root>(uri);

            return json.Summaries.Count;
        }

        public async Task<Datum> GetEmployeeById(Guid employeeId)
        {
            var uri = $"{ApiUrlBase}/employees/{employeeId}";

            return await _requestProvider.GetAsync<Datum>(uri);
        }

        public async Task<Datum> AddEmployee(Datum dto)
        {
            var uri = $"{ApiUrlBase}/employee/add";

            return await _requestProvider.PostAsync(uri, dto);
        }

        public async Task<Datum> UpdateEmployee(Datum dto, string employeeId)
        {
            var uri = $"{ApiUrlBase}/employees/employeeId/update";

            return await _requestProvider.PostAsync(uri, dto);
        }
    }
}