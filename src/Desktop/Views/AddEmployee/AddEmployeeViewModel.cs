using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.AddEmployee
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IPositionsService _positionsService;
        private readonly IShellManager _shellManager;

        private ObservableCollection<Models.Position> _positions;
        private string _positionId;

        private string _employeeDateOfBirth;

        private bool _employeeIsForeman;
        private string _employeeLastName;

        private string _employeeName;
        private string _employeeNationality;
        private bool _employeeReadDrawings;
        private string _employeeSecondName;

        public AddEmployeeViewModel(IEmployeesService employeesService, IPositionsService positionsService, IShellManager shellManager)
        {
            _employeesService = employeesService;
            _positionsService = positionsService;
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

        public ObservableCollection<Models.Position> Positions
        {
            get => _positions;
            set => Set(ref _positions, value);
        }

        public string PositionId
        {
            get => _positionId;
            set => Set(ref _positionId, value);
        }

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(EmployeeName) || string.IsNullOrWhiteSpace(EmployeeLastName) ||
                string.IsNullOrWhiteSpace(EmployeeDateOfBirth) ||
                string.IsNullOrWhiteSpace(EmployeeDateOfBirth)) return new ValidationResult(false);

            return new ValidationResult(true);
        }

        public async Task Initialize()
        {
            Positions = await _positionsService.GetAllPositions();
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
                        PositionId = PositionId,
                        Name = EmployeeName,
                        SecondName = EmployeeSecondName,
                        LastName = EmployeeLastName,
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