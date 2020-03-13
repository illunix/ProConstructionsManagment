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

        private string _employeeDateOfBirth;

        private bool _employeeIsForeman;
        private string _employeeLastName;

        private string _employeeName;
        private string _employeeNationality;
        private bool _employeeReadDrawings;
        private string _employeeSecondName;

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

        public string EmployeeSecondName
        {
            get => _employeeSecondName;
            set => Set(ref _employeeSecondName, value);
        }

        public string EmployeeLastName
        {
            get => _employeeLastName;
            set => Set(ref _employeeLastName, value);
        }

        public string EmployeeDateOfBirth
        {
            get => _employeeDateOfBirth;
            set => Set(ref _employeeDateOfBirth, value);
        }

        public string EmployeeNationality
        {
            get => _employeeNationality;
            set => Set(ref _employeeNationality, value);
        }

        public bool EmployeeIsForeman
        {
            get => _employeeIsForeman;
            set => Set(ref _employeeIsForeman, value);
        }

        public bool EmployeeReadDrawings
        {
            get => _employeeReadDrawings;
            set => Set(ref _employeeReadDrawings, value);
        }

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(EmployeeName) || string.IsNullOrWhiteSpace(EmployeeLastName) ||
                string.IsNullOrWhiteSpace(EmployeeDateOfBirth) ||
                string.IsNullOrWhiteSpace(EmployeeDateOfBirth)) return new ValidationResult(false);

            return new ValidationResult(true);
        }

        public ICommand AddEmployeeCommand => new AsyncRelayCommand(AddEmployee);

        private async Task AddEmployee()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var data = new Models.Employee
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = EmployeeName,
                        SecondName = EmployeeSecondName,
                        LastName =  EmployeeLastName,
                        DateOfBirth = EmployeeDateOfBirth,
                        Nationality = EmployeeNationality,
                        IsForeman = EmployeeIsForeman,
                        ReadDrawings = EmployeeReadDrawings,
                        Status = 0
                    };

                    var result = await Task.Run(() => _employeesService.AddEmployee(data));
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully added new employee ({data.Id})");
                        
                        MessageBox.Show("Pomyślnie dodano pracownika");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed adding new employee");

                    MessageBox.Show(
                        "Coś poszło nie tak podczas dodawania pracownika, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
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