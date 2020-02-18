using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.AddEmployee
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IShellManager _shellManager;
        private string _employeeLastName;

        private string _employeeName;
        private bool _employeeReadDrawings;

        public AddEmployeeViewModel(IEmployeesService employeesService, IShellManager shellManager)
        {
            _employeesService = employeesService;
            _shellManager = shellManager;
        }

        public string EmployeeName
        {
            get => _employeeName;
            set => Set(ref _employeeName, value);
        }

        public string EmployeeLastName
        {
            get => _employeeLastName;
            set => Set(ref _employeeLastName, value);
        }

        public string EmployeeDateOfBirth { get; set; }

        public bool EmployeeIsForeman { get; set; }

        public bool EmployeeReadDrawings
        {
            get => _employeeReadDrawings;
            set => Set(ref _employeeReadDrawings, value);
        }

        public ICommand EmployeeAddCommand => new AsyncRelayCommand(EmployeeAdd);

        private async Task EmployeeAdd()
        {
            _shellManager.SetLoadingData(true);

            var data = new Models.Employee
            {
                Id = Guid.NewGuid().ToString(),
                Name = EmployeeName,
                DateOfBirth = EmployeeDateOfBirth,
                LastName = EmployeeLastName,
                IsForeman = EmployeeIsForeman,
                ReadDrawings = EmployeeReadDrawings
            };

            await _employeesService.AddEmployee(data);

            _shellManager.SetLoadingData(false);

            MessageBox.Show("Pomyślnie dodano pracownika");
        }
    }
}