using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.Employee
{
    public class EmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;
        
        private string _employeeDateOfBirth;
        private string _employeeId;
        private bool _employeeIsForeman;
        private string _employeeLastName;
        private string _employeeName;

        private string _employeeNationality;
        private bool _employeeReadDrawings;
        private string _employeeSecondName;

        public EmployeeViewModel(IEmployeesService employeesService, IShellManager shellManager,
            IMessengerService messengerService)
        {
            _employeesService = employeesService;
            _shellManager = shellManager;
            _messengerService = messengerService;

            messengerService.Register<EmployeeIdMessage>(this, msg => EmployeeId = msg.EmployeeId);
        }
        
        public string EmployeeId
        {
            get => _employeeId;
            set => Set(ref _employeeId, value);
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

        public ICommand UpdateEmployeeCommand => new AsyncRelayCommand(UpdateEmployee);

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(EmployeeName) || string.IsNullOrWhiteSpace(EmployeeLastName) ||
                string.IsNullOrWhiteSpace(EmployeeDateOfBirth) ||
                string.IsNullOrWhiteSpace(EmployeeNationality))
            {
                MessageBox.Show("Uzupełnij wymagane pola");

                return new ValidationResult(false);
            }
            
            return new ValidationResult(true);
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                var employee = await _employeesService.GetEmployeeById(EmployeeId);

                EmployeeName = employee.Name;
                EmployeeSecondName = employee.SecondName;
                EmployeeLastName = employee.LastName;
                EmployeeDateOfBirth = employee.DateOfBirth;
                EmployeeNationality = employee.Nationality;
                EmployeeIsForeman = employee.IsForeman;
                EmployeeReadDrawings = employee.ReadDrawings;
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading employee view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }

        private async Task UpdateEmployee()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var employee = await _employeesService.GetEmployeeById(EmployeeId);

                    var data = new Models.Employee
                    {
                        Id = EmployeeId,
                        Name = EmployeeName,
                        SecondName = EmployeeSecondName,
                        LastName = EmployeeLastName,
                        DateOfBirth = EmployeeDateOfBirth,
                        Nationality = EmployeeNationality,
                        IsForeman = EmployeeIsForeman,
                        ReadDrawings = EmployeeReadDrawings,
                        Status = employee.Status
                    };

                    var result = _employeesService.UpdateEmployee(data, EmployeeId);
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully updated employee ({data.Id})");

                        MessageBox.Show("Pomyślnie zapisano zmiany");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed updating employee");

                    MessageBox.Show(
                        "Coś poszło nie tak podczas zapisywania zmian, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
                }
                finally
                {
                    _shellManager.SetLoadingData(false);
                }
            }
        }
    }
}