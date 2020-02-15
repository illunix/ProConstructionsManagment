using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.DTO;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.AddEmployee
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IShellManager _shellManager;

        public AddEmployeeViewModel(IEmployeesService employeesService, IShellManager shellManager)
        {
            _employeesService = employeesService;
            _shellManager = shellManager;
        }

        private string _employeeName;
        private string _employeeLastName;
        private string _employeeDateOfBirth;
        private bool _employeeIsForeman;
        private bool _employeeReadDrawings;

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

        public string EmployeeDateOfBirth
        {
            get => _employeeDateOfBirth;
            set => _employeeDateOfBirth = value;
        }

        public bool EmployeeIsForeman
        {
            get => _employeeIsForeman;
            set => _employeeIsForeman = value;
        }

        public bool EmployeeReadDrawings
        {
            get => _employeeReadDrawings;
            set => Set(ref _employeeReadDrawings, value);
        }

        public ICommand EmployeeAddCommand => new AsyncRelayCommand(EmployeeAdd);

        private async Task EmployeeAdd()
        {
            _shellManager.SetLoadingData(true);

            var data = new Datum
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
