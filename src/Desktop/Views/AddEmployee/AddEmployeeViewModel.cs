using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.AddEmployee
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IShellManager _shellManager;
        private string _employeeLastName;

        private string _employeeName;
        private bool _employeeReadDrawings;
        private bool _showEmployeeLastNameHighlighted;

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

        public bool ShowEmployeeLastNameHighlighted
        {
            get => _showEmployeeLastNameHighlighted; 
            set => Set(ref _showEmployeeLastNameHighlighted, value);
        }
        
        public ICommand EmployeeAddCommand => new AsyncRelayCommand(EmployeeAdd);
        
        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(EmployeeLastName))
            {
                ShowEmployeeLastNameHighlighted = true;
            }

            if (string.IsNullOrWhiteSpace(EmployeeName) || string.IsNullOrWhiteSpace(EmployeeLastName) || string.IsNullOrWhiteSpace(EmployeeDateOfBirth) || string.IsNullOrWhiteSpace(EmployeeDateOfBirth))
            {
                return new ValidationResult(false);
            }
            
            return new ValidationResult(true);
        }

        private async Task EmployeeAdd()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var data = new Models.Employee
                    {
                        Name = EmployeeName,
                        DateOfBirth = EmployeeDateOfBirth,
                        LastName = EmployeeLastName,
                        IsForeman = EmployeeIsForeman,
                        ReadDrawings = EmployeeReadDrawings
                    };

                    var result = _employeesService.AddEmployee(data);
                    if (result.IsSuccessful)
                    {
                        MessageBox.Show("Pomyślnie dodano pracownika");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed adding new employee");

                    MessageBox.Show("Coś poszło nie tak podczas zapisywania zmian, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
                }
                finally
                {
                    _shellManager.SetLoadingData(false);
                }
            }
            else
            {
                MessageBox.Show("Uzupełnij wymagane pola");
            }
        }
    }
}